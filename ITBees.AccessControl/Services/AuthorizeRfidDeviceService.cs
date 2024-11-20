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

class AuthorizeRfidDeviceService : IAuthorizeRfidDeviceService
{
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IWriteOnlyRepository<RfidReaderDevice> _rfidReaderDeviceRwRepo;
    private readonly IWriteOnlyRepository<UnauthorizedRfidDevice> _unauthorizedRfidDeviceRwRepo;
    private readonly IWriteOnlyRepository<AuthorizedAgent> _authorizedAgentRwRepo;
    private readonly IWriteOnlyRepository<AwaitingAgent> _awaitingAgentRwRepo;
    private readonly IReadOnlyRepository<UnauthorizedRfidDevice> _unauthorizedRfidDeviceRoRepo;
    private readonly IWriteOnlyRepository<IpAddress> _ipAddressRwRepo;

    public AuthorizeRfidDeviceService(IAspCurrentUserService aspCurrentUserService,
        IWriteOnlyRepository<RfidReaderDevice> rfidReaderDeviceRwRepo,
        IWriteOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRwRepo,
        IWriteOnlyRepository<AuthorizedAgent> authorizedAgentRwRepo,
        IWriteOnlyRepository<AwaitingAgent> awaitingAgentRwRepo,
        IReadOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRoRepo,
        IWriteOnlyRepository<IpAddress> ipAddressRwRepo)
    {
        _aspCurrentUserService = aspCurrentUserService;
        _rfidReaderDeviceRwRepo = rfidReaderDeviceRwRepo;
        _unauthorizedRfidDeviceRwRepo = unauthorizedRfidDeviceRwRepo;
        _authorizedAgentRwRepo = authorizedAgentRwRepo;
        _awaitingAgentRwRepo = awaitingAgentRwRepo;
        _unauthorizedRfidDeviceRoRepo = unauthorizedRfidDeviceRoRepo;
        _ipAddressRwRepo = ipAddressRwRepo;
    }
    public RfidReaderDevice Authorize(AuthorizeRfidDeviceIm authorizeRfidDeviceIm)
    {
        try
        {
            var awaitingAgent = _unauthorizedRfidDeviceRoRepo.GetData(x => x.Mac == authorizeRfidDeviceIm.Mac).First();

            var ip = _ipAddressRwRepo.InsertData(new IpAddress()
            {
                IpNetworkAddressId = authorizeRfidDeviceIm.IpNetworkAddressId,
                Ip = authorizeRfidDeviceIm.Ip,
                IpVersion = IpVersionType.IPv4,
                IsActive = true,
                CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
            });

            var result = _rfidReaderDeviceRwRepo.InsertData(new RfidReaderDevice()
            {
                Mac = authorizeRfidDeviceIm.Mac,
                Ip = authorizeRfidDeviceIm.Ip,
                CompanyGuid = authorizeRfidDeviceIm.CompanyGuid,
                DeviceName = authorizeRfidDeviceIm.DeviceName,
                LastConnection = null,
                BuildingGuid = authorizeRfidDeviceIm.BuildingGuid,
                IpAddressId = ip.Id,
                TriggerApiEndpoint = authorizeRfidDeviceIm.TriggerApiEndpoint,

            });

            _unauthorizedRfidDeviceRwRepo.DeleteData(x => x.Mac == authorizeRfidDeviceIm.Mac);
            _awaitingAgentRwRepo.DeleteData(x => x.Mac == authorizeRfidDeviceIm.Mac);
            _authorizedAgentRwRepo.InsertData(new AuthorizedAgent()
            {
                Mac = authorizeRfidDeviceIm.Mac,
                AcceptedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
                AcceptedDate = DateTime.Now,
                BuildingGuid = authorizeRfidDeviceIm.BuildingGuid,
                CompanyGuid = authorizeRfidDeviceIm.CompanyGuid,
                Description = authorizeRfidDeviceIm.DeviceName,
                DisplayName = authorizeRfidDeviceIm.DeviceName,
                LastConnectedDate = awaitingAgent.FirstSeenDate,
                DeviceGuid = authorizeRfidDeviceIm.DeviceGuid,
                SecretKey = string.Empty,
                SystemInformation = $"Internal IP : {awaitingAgent.IpForwarded} external IP :{awaitingAgent.Ip}"
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