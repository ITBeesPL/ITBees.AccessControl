using ITBees.Models.Buildings;
using ITBees.Models.Interfaces;

namespace ITBees.AccessControl.Controllers.PlatformAdmin.Models;

public class BuildingVm
{
    public BuildingVm()
    {
        
    }

    public BuildingVm(Building x)
    {
        Guid = x.Guid;
        BuildingDeviceHubs = x.BuildingDeviceHubs.Select(x=>new BuildingDeviceHubVm(x)).ToList();
        GpsLocation = new GpsLocationVm(x.GpsLocation);
        Name = x.Name;
        CreatedBy = x.CreatedBy?.DisplayName;
        Created = x.Created;
        IsActive = x.IsActive;
    }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public GpsLocationVm GpsLocation { get; set; }
    public List<BuildingDeviceHubVm> BuildingDeviceHubs { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
}