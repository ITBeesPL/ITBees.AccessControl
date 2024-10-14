using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.Interfaces.Repository;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class BuildingsController : RestfulControllerBase<BuildingsController>
{
    private readonly IBuildingService _buildingService;

    public BuildingsController(ILogger<BuildingsController> logger, IBuildingService buildingService) : base(logger)
    {
        _buildingService = buildingService;
    }

    [HttpGet]
    [Produces<PaginatedResult<BuildingVm>>]
    public IActionResult Get(Guid companyGuid, int page, int pageSize, string sortColumn, SortOrder sortOrder)
    {
        return ReturnOkResult(() => _buildingService.GetAll(companyGuid, page,pageSize, sortColumn, sortOrder));
    }
}