using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;
using ITBees.RestfulApiControllers.Exceptions;

namespace ITBees.AccessControl.Interfaces;

class AccessControlSignalReceived : IAccessControlSignalReceived
{
    private readonly IReadOnlyRepository<AccessCard> _accessCardRoRepo;
    private readonly IWriteOnlyRepository<UnauthorizedAccessCardLog> _unauthorizedAccessCardLog;
    private readonly IReadOnlyRepository<RfidReaderDevice> _rfidReaderDeviceRoRepo;
    private readonly IUnauthorizedRfidDevicesService _unauthorizedRfidDevicesService;

    public AccessControlSignalReceived(IReadOnlyRepository<AccessCard> accessCardRoRepo,
        IWriteOnlyRepository<UnauthorizedAccessCardLog> unauthorizedAccessCardLog,
        IReadOnlyRepository<RfidReaderDevice> rfidReaderDeviceRoRepo,
        IUnauthorizedRfidDevicesService unauthorizedRfidDevicesService)
    {
        _accessCardRoRepo = accessCardRoRepo;
        _unauthorizedAccessCardLog = unauthorizedAccessCardLog;
        _rfidReaderDeviceRoRepo = rfidReaderDeviceRoRepo;
        _unauthorizedRfidDevicesService = unauthorizedRfidDevicesService;
    }

    public AccessRequestResultVm Handle(ReceivedRfidSignalIm receivedRfidSignalIm, string ip)
    {
        var rfidDevice = _rfidReaderDeviceRoRepo.GetData(x => x.Ip == receivedRfidSignalIm.Ip).FirstOrDefault();
        if (rfidDevice == null)
        {
            _unauthorizedRfidDevicesService.Create(new UnauthorizedRfidDevice()
            {
                Ip = receivedRfidSignalIm.Ip,
                DeviceName = receivedRfidSignalIm.Name,
                FirstSeenDate = DateTime.Now,
                IpForwarded = ip,
                Mac = receivedRfidSignalIm.Mac
            });

            throw new ResultNotFoundException("Rfid device is not authorized.");
        }

        if (rfidDevice.Mac != receivedRfidSignalIm.Mac)
        {
            throw new ResultNotFoundException("Rfid device is not authorized hardware address incorrect.");
        }

        var savedAccessCard = _accessCardRoRepo.GetFirst(x => x.CardId == receivedRfidSignalIm.Id);
        if (savedAccessCard == null)
        {
            var message = "Card not found";
            LogCardRequestInactive(receivedRfidSignalIm,rfidDevice.Guid,message);
            throw new UnauthorizedAccessException(message);
        }

        if (savedAccessCard.IsActive == false)
        {
            var message = "Card not active";
            LogCardRequestInactive(receivedRfidSignalIm, rfidDevice.Guid, message);
            throw new UnauthorizedAccessException(message);
        }

        if (savedAccessCard.ValidDate < DateTime.Now)
        {
            var message = "Card not expired";
            LogCardRequestInactive(receivedRfidSignalIm, rfidDevice.Guid, message);
            throw new UnauthorizedAccessException(message);
        }

        return new AccessRequestResultVm() { Message = "ok" };
    }

    private void LogCardRequestInactive(ReceivedRfidSignalIm receivedRfidSignalIm, Guid connectedTroughRfidReaderDeviceGuid, string message)
    {
        _unauthorizedAccessCardLog.InsertData(new UnauthorizedAccessCardLog()
        {
            CardId = receivedRfidSignalIm.Id,
            EventDate = DateTime.Now,
            LastConnectedTroughRfidReaderDeviceGuid = connectedTroughRfidReaderDeviceGuid,
            Message = message
        });
    }
}