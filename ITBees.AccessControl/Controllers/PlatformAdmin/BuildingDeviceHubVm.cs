namespace ITBees.AccessControl.Controllers.PlatformAdmin;

public class BuildingDeviceHubVm
{
    public Guid Guid { get; set; }
    public Guid BuildingGuid { get; set; }
    public DeviceHubVm DeviceHub { get; set; }
    public Guid DeviceHubGuid { get; set; }
}