using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
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

    public AuthorizeRfidDeviceService(IAspCurrentUserService aspCurrentUserService,
        IWriteOnlyRepository<RfidReaderDevice> rfidReaderDeviceRwRepo,
        IWriteOnlyRepository<UnauthorizedRfidDevice> unauthorizedRfidDeviceRwRepo)
    {
        _aspCurrentUserService = aspCurrentUserService;
        _rfidReaderDeviceRwRepo = rfidReaderDeviceRwRepo;
        _unauthorizedRfidDeviceRwRepo = unauthorizedRfidDeviceRwRepo;
    }
    public RfidReaderDevice Authorize(AuthorizeRfidDeviceIm authorizeRfidDeviceIm)
    {
        try
        {
            var result = _rfidReaderDeviceRwRepo.InsertData(new RfidReaderDevice()
            {
                Mac = authorizeRfidDeviceIm.Mac,
                Ip = authorizeRfidDeviceIm.Ip,
                CompanyGuid = authorizeRfidDeviceIm.CompanyGuid,
                DeviceName = authorizeRfidDeviceIm.DeviceName,
                LastConnection = null,
                BuildingGuid =authorizeRfidDeviceIm.BuildingGuid
            });

            _unauthorizedRfidDeviceRwRepo.DeleteData(x => x.Mac == authorizeRfidDeviceIm.Mac);
            
            return result;
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Duplicate"))
            {
                throw new FasApiErrorException(new FasApiErrorVm("Rfid reader already in database", StatusCodes.Status400BadRequest, ""));
            }

            throw new FasApiErrorException(new FasApiErrorVm(e.Message, StatusCodes.Status500InternalServerError, ""));
        }
    }
}