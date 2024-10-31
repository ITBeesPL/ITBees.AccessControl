using ITBees.AccessControl.Services.PlatformOperator;
using ITBees.Interfaces.Repository;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformOperator;

public class AcGroupsAccessCardsController : RestfulControllerBase<AcGroupsAccessCardsController>
{
    private readonly IAcGroupAccessCardsService _acGroupAccessCardsService;

    public AcGroupsAccessCardsController(ILogger<AcGroupsAccessCardsController> logger,
        IAcGroupAccessCardsService acGroupAccessCardsService) : base(logger)
    {
        _acGroupAccessCardsService = acGroupAccessCardsService;
    }

    [HttpGet]
    [Produces<PaginatedResult<AcGroupAccessCardsVm>>]
    public IActionResult Get(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        return ReturnOkResult(() => { _acGroupAccessCardsService.GetAll(page, pageSize, sortColumn, sortOrder); });
    }
}