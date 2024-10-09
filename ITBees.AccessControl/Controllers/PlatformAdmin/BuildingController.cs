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