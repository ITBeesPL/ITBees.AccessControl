using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.Models.Common;
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

    [HttpPost]
    [Produces<BuildingVm>]
    public IActionResult Post([FromBody] BuildingIm buildingIm)
    {
        return ReturnOkResult(() => _buildingService.Create(buildingIm));
    }

    [HttpPut]
    [Produces<BuildingVm>]
    public IActionResult Put([FromBody] BuildingUm buildingUm)
    {
        return ReturnOkResult(() => _buildingService.Update(buildingUm));
    }

    [HttpDelete]
    public IActionResult Del(Guid guid)
    {
        return ReturnOkResult(()=>_buildingService.Delete(guid));
    }
}