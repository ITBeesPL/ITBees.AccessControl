using ITBees.AccessControl.Interfaces.ViewModels;

namespace ITBees.AccessControl.Interfaces;

public interface IAccessControlSignalReceived
{
    AccessRequestResultVm Handle(ReceivedRfidSignalIm receivedRfidSignalIm, string ip);
}