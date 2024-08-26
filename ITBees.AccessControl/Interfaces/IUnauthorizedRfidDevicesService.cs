using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;

namespace ITBees.AccessControl.Interfaces;

public interface IUnauthorizedRfidDevicesService
{
    List<UnauthorizedRfidDeviceVm> Get();
    UnauthorizedRfidDeviceVm Create(UnauthorizedRfidDevice unauthorizedRfidDevice);
}