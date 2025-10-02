using ITBees.AccessControl.Services.PlatformOperator;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware.Ac;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformOperator;

[Authorize]
public class AcGroupsController : RestfulControllerBase<AcGroupsController>
{
    private readonly IAcGroupsService _acGroupsService;

    public AcGroupsController(ILogger<AcGroupsController> logger,
        IAcGroupsService acGroupsService) : base(logger)
    {
        _acGroupsService = acGroupsService;
    }

    [HttpGet]
    [Produces<PaginatedResult<AcGroupVm>>]
    public IActionResult Get(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        return ReturnOkResult(() => _acGroupsService.GetAll(page, pageSize, sortColumn, sortOrder));
    }
}