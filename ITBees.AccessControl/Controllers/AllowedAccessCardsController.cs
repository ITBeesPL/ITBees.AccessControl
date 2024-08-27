using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers;

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
    public IActionResult Post([FromBody] AllowedAccessCardsIm allowedAccessCardIm)
    {
        return ReturnOkResult(() => _allowedCardsService.RegisterCard(allowedAccessCardIm));
    }
}