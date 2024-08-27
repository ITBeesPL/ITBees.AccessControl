using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformInfrastructure;

[Authorize]
public class AccessCardsController : RestfulControllerBase<AccessCardsController>
{
    private readonly IAccessCardsService _accessCardsService;

    public AccessCardsController(ILogger<AccessCardsController> logger,
        IAccessCardsService accessCardsService) : base(logger)
    {
        _accessCardsService = accessCardsService;
    }

    [HttpGet]
    [Produces<AccessCardsVm>]
    public IActionResult Get(int page, int pageSize, string sortColumn, SortOrder sortOrder)
    {
        return ReturnOkResult(()=>_accessCardsService.GetMyCompanyCards(page, pageSize, sortColumn, sortOrder));
    }
}