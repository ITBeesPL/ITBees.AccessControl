using ITBees.RestClient.Interfaces.RestModelMarkup;

namespace ITBees.AccessControl.Controllers.PlatformOperator.Models;

public class AccessCardStatusVm : Vm
{
    public bool IsAllowedToActivate { get; set; }
    public bool IsAlreadyActivated { get; set; }
    public string Message { get; set; }
}