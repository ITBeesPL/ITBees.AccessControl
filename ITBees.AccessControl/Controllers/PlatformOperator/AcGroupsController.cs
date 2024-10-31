using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.AccessControl.Services.PlatformOperator;
using ITBees.Interfaces.Repository;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformOperator;

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
        return ReturnOkResult(() => { _acGroupsService.GetAll(page, pageSize, sortColumn, sortOrder); });
    }
}