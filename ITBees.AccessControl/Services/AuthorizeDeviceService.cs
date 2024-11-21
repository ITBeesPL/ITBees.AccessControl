using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.FAS.SatelliteAgents.Database;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware.Infrastructure;
using ITBees.RestfulApiControllers.Exceptions;
using ITBees.RestfulApiControllers.Models;
using ITBees.UserManager.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ITBees.AccessControl.Services;

class AuthorizeDeviceService : IAuthorizeRfidDeviceService
{
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IWriteOnlyRepository<RfidReaderDevice> _rfidReaderDeviceRwRepo;
    private readonly IWriteOnlyRepository<Device> _deviceRwRepo;
    private readonly IWriteOnlyRepository<UnauthorizedRfidDevice> _unauthorizedRfidDeviceRwRepo;
    private readonly IWriteOnlyRepository<AuthorizedAgent> _authorizedAgentRwRepo;
    private readonly IWriteOnlyRepository<AwaitingAgent> _awaitingAgentRwRepo;
    private readonly IReadOnlyRepository<AwaitingAgent> _awaitingAgentRoRepo;
    private readonly IReadOnlyRepository<UnauthorizedRfidDevice> _unauthorizedRfidDeviceRoRepo;
    private readonly IWriteOnlyRepository<IpAddress> _ipAddressRwRepo;

    public AuthorizeDeviceService(IAspCurrentUserService aspCurrentUserService,
        IWriteOnlyRepository<RfidReaderDevice> rfidReaderDeviceRwRepo,
        IWriteOnlyRepository<Device> deviceRwRepo,
        IWriteOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRwRepo,
        IWriteOnlyRepository<AuthorizedAgent> authorizedAgentRwRepo,
        IWriteOnlyRepository<AwaitingAgent> awaitingAgentRwRepo,
        IReadOnlyRepository<AwaitingAgent> awaitingAgentRoRepo,
        IReadOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRoRepo,
        IWriteOnlyRepository<IpAddress> ipAddressRwRepo)
    {
        _aspCurrentUserService = aspCurrentUserService;
        _rfidReaderDeviceRwRepo = rfidReaderDeviceRwRepo;
        _deviceRwRepo = deviceRwRepo;
        _unauthorizedRfidDeviceRwRepo = unauthorizedRfidDeviceRwRepo;
        _authorizedAgentRwRepo = authorizedAgentRwRepo;
        _awaitingAgentRwRepo = awaitingAgentRwRepo;
        _awaitingAgentRoRepo = awaitingAgentRoRepo;
        _unauthorizedRfidDeviceRoRepo = unauthorizedRfidDeviceRoRepo;
        _ipAddressRwRepo = ipAddressRwRepo;
    }
    public RfidReaderDevice Authorize(AuthorizeDeviceIm authorizeDeviceIm)
    {
        try
        {
            var unauthorizedRfidDevice = _unauthorizedRfidDeviceRoRepo.GetData(x => x.Mac == authorizeDeviceIm.Mac).FirstOrDefault();
            var awaitingAgent = _awaitingAgentRoRepo.GetData(x => x.Mac == authorizeDeviceIm.Mac).FirstOrDefault();

            var ip = _ipAddressRwRepo.InsertData(new IpAddress()
            {
                IpNetworkAddressId = authorizeDeviceIm.IpNetworkAddressId,
                Ip = authorizeDeviceIm.Ip,
                IpVersion = IpVersionType.IPv4,
                IsActive = true,
                CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
            });

            var result = _rfidReaderDeviceRwRepo.InsertData(new RfidReaderDevice()
            {
                Mac = authorizeDeviceIm.Mac,
                Ip = authorizeDeviceIm.Ip,
                CompanyGuid = authorizeDeviceIm.CompanyGuid,
                DeviceName = authorizeDeviceIm.DeviceName,
                LastConnection = null,
                BuildingGuid = authorizeDeviceIm.BuildingGuid,
                IpAddressId = ip.Id,
                TriggerApiEndpoint = authorizeDeviceIm.TriggerApiEndpoint,

            });

            _unauthorizedRfidDeviceRwRepo.DeleteData(x => x.Mac == authorizeDeviceIm.Mac);
            _awaitingAgentRwRepo.DeleteData(x => x.Mac == authorizeDeviceIm.Mac);
           
            var ipForwarded = string.Empty;
            var externalIp = string.Empty;
            var systemInformation = string.Empty;

            if (unauthorizedRfidDevice != null)
            {
                ipForwarded = unauthorizedRfidDevice.IpForwarded;
                externalIp = unauthorizedRfidDevice.Ip;
                systemInformation = $"Internal IP : {ipForwarded} external IP :{externalIp}";
            }
            else
            {
                systemInformation = awaitingAgent.SystemInformation;
            }

            var firstSeenDate = awaitingAgent.LastConnectedDate;

            _authorizedAgentRwRepo.InsertData(new AuthorizedAgent()
            {
                Mac = authorizeDeviceIm.Mac,
                AcceptedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
                AcceptedDate = DateTime.Now,
                BuildingGuid = authorizeDeviceIm.BuildingGuid,
                CompanyGuid = authorizeDeviceIm.CompanyGuid,
                Description = authorizeDeviceIm.DeviceName,
                DisplayName = authorizeDeviceIm.DeviceName,
                LastConnectedDate = firstSeenDate,
                DeviceGuid = authorizeDeviceIm.DeviceGuid,
                SecretKey = string.Empty,
                SystemInformation = systemInformation,
            });

            return result;
        }
        catch (Exception e)
        {
            if (e.InnerException.Message.Contains("Duplicate"))
            {
                throw new FasApiErrorException(new FasApiErrorVm("Rfid reader already in database", StatusCodes.Status400BadRequest, ""));
            }

            throw new FasApiErrorException(new FasApiErrorVm(e.Message, StatusCodes.Status500InternalServerError, ""));
        }
    }
}