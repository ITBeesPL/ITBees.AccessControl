using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;

namespace ITBees.AccessControl.Interfaces;

public interface IAuthorizeRfidDeviceService
{
    RfidReaderDevice Authorize(AuthorizeRfidDeviceIm authorizeRfidDeviceIm);
}