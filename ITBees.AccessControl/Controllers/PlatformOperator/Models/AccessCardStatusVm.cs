namespace ITBees.AccessControl.Controllers.PlatformOperator.Models;

public class AccessCardStatusVm
{
    public bool IsAllowedToActivate { get; set; }
    public bool IsAlreadyActivated { get; set; }
    public string Message { get; set; }
}