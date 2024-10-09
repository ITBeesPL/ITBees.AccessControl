using ITBees.Models.Interfaces;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

public class BuildingVm
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public GpsLocationVm GpsLocation { get; set; }
    public List<BuildingDeviceHubVm> BuildingDeviceHubs { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
}