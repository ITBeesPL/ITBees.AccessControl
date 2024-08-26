using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Services;

class UnauthorizedRfidDevicesService : IUnauthorizedRfidDevicesService
{
    private readonly IReadOnlyRepository<UnauthorizedRfidDevice> _unauthorizedRfidDeviceRoRepo;
    private readonly IWriteOnlyRepository<UnauthorizedRfidDevice> _unauthorizedRfidDeviceRwRepo;

    public UnauthorizedRfidDevicesService(
        IReadOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRoRepo,
        IWriteOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRwRepo
        )
    {
        _unauthorizedRfidDeviceRoRepo = unauthorizedRfidDeviceRoRepo;
        _unauthorizedRfidDeviceRwRepo = unauthorizedRfidDeviceRwRepo;
    }
    public List<UnauthorizedRfidDeviceVm> Get()
    {
        return _unauthorizedRfidDeviceRoRepo.GetData(x => true).Select(x => new UnauthorizedRfidDeviceVm(x)).ToList();
    }

    public UnauthorizedRfidDeviceVm Create(UnauthorizedRfidDevice unauthorizedRfidDevice)
    {
        UnauthorizedRfidDevice result = null;
        try
        {
            result = _unauthorizedRfidDeviceRwRepo.InsertData(unauthorizedRfidDevice);
        }
        catch (Exception e)
        {
            result = _unauthorizedRfidDeviceRwRepo.UpdateData(x => x.Mac == unauthorizedRfidDevice.Mac, x =>
            {
                x.LastConnection = DateTime.Now;
            }).FirstOrDefault()!;
        }

        return new UnauthorizedRfidDeviceVm(result);
    }
}