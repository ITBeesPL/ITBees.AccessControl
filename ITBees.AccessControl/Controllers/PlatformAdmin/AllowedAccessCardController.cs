using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class AllowedAccessCardController : RestfulControllerBase<AllowedAccessCardController>
{
    private readonly IAllowedCardsService _allowedCardsService;

    public AllowedAccessCardController(ILogger<AllowedAccessCardController> logger,
        IAllowedCardsService allowedCardsService) : base(logger)
    {
        _allowedCardsService = allowedCardsService;
    }

    [HttpPut]
    [Produces<AllowedAccessCardsVm>]
    public IActionResult Put([FromBody] AllowedAccessCardUm allowedAccessCardUm)
    {
        return ReturnOkResult(() => _allowedCardsService.UpdateCard(allowedAccessCardUm));
    }
}