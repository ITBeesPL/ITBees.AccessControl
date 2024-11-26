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
        BuildingDeviceHubs = x.BuildingDeviceHubs?.Select(x=>new BuildingDeviceHubVm(x)).ToList();
        GpsLocation = x.GpsLocation != null ? new GpsLocationVm(x.GpsLocation) : null;
        Name = x.Name;
        CreatedBy = x.CreatedBy?.DisplayName;
        Created = x.Created;
        IsActive = x.IsActive;
        CompanyGuid = x.CompanyGuid;
        CompanyName = x.Company?.CompanyName;
    }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public GpsLocationVm GpsLocation { get; set; }
    public List<BuildingDeviceHubVm> BuildingDeviceHubs { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public Guid? CompanyGuid { get; set; }
    public string? CompanyName { get; set; }
}