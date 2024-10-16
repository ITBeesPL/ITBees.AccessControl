using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.Models.Interfaces;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

public class BuildingUm
{
    public GpsLocationVm GpsLocation { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public Guid Guid { get; set; }
    public Guid? CompanyGuid { get; set; }
    public List<BuildingDeviceHubVm> BuildingDeviceHubs { get; set; }
}