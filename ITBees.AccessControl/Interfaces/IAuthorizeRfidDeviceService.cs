using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Interfaces;

public interface IAuthorizeRfidDeviceService
{
    RfidReaderDevice Authorize(AuthorizeDeviceIm authorizeDeviceIm);
}