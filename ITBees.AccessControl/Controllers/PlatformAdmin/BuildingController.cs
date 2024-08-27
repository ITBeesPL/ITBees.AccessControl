using ITBees.Models.Interfaces;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class BuildingController : RestfulControllerBase<BuildingController>
{
    private readonly IBuildingService _buildingService;

    public BuildingController(ILogger<BuildingController> logger, IBuildingService buildingService) : base(logger)
    {
        _buildingService = buildingService;
    }

    [HttpGet]
    [Produces<BuildingVm>]
    public IActionResult Get(Guid guid)
    {
        return ReturnOkResult(() => _buildingService.Get(guid));
    }
}

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

public class BuildingDeviceHubVm
{
    public Guid Guid { get; set; }
    public BuildingVm Building { get; set; }
    public Guid BuildingGuid { get; set; }
    public DeviceHubVm DeviceHub { get; set; }
    public Guid DeviceHubGuid { get; set; }
}

public class DeviceHubVm
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
}

public interface IBuildingService
{
    BuildingVm Get(Guid guid);
}