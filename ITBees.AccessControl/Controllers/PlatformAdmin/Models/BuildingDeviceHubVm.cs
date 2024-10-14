using ITBees.Models.Buildings;

namespace ITBees.AccessControl.Controllers.PlatformAdmin.Models;

public class BuildingDeviceHubVm
{
    public BuildingDeviceHubVm()
    {
        
    }

    public BuildingDeviceHubVm(BuildingDeviceHub x)
    {
        Guid = x.Guid;
        BuildingGuid = x.BuildingGuid;
        DeviceHub = new DeviceHubVm(x.DeviceHub);
        DeviceHubGuid = x.DeviceHubGuid;
    }
    public Guid Guid { get; set; }
    public Guid BuildingGuid { get; set; }
    public DeviceHubVm DeviceHub { get; set; }
    public Guid DeviceHubGuid { get; set; }
}