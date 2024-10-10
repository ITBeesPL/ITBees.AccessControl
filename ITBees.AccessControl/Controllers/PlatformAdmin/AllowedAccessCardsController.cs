using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class AllowedAccessCardsController : RestfulControllerBase<AllowedAccessCardsController>
{
    private readonly IAllowedCardsService _allowedCardsService;

    public AllowedAccessCardsController(ILogger<AllowedAccessCardsController> logger,
        IAllowedCardsService allowedCardsService) : base(logger)
    {
        _allowedCardsService = allowedCardsService;
    }

    [HttpPost]
    [Produces<AllowedAccessCardsVm>]
    public IActionResult Post([FromBody] AllowedAccessCardsIm allowedAccessCardIm)
    {
        return ReturnOkResult(() => _allowedCardsService.RegisterCard(allowedAccessCardIm));
    }

    [HttpGet]
    [Produces<PaginatedResult<AllowedAccessCardsVm>>]
    public IActionResult Get(int page, int pageSize, string sortColumn, SortOrder sortOrder)
    {
        return ReturnOkResult(() => _allowedCardsService.GetCards(page, pageSize, sortColumn, sortOrder));
    }
}