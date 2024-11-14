using ITBees.AccessControl.Controllers.PlatformOperator.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformOperator;

[Authorize]
public class DeactivateAccessCardsController : RestfulControllerBase<DeactivateAccessCardsController>
{
    private readonly IDeactivateAccessCardsService _deactivateAccessCardsService;

    public DeactivateAccessCardsController(
        ILogger<DeactivateAccessCardsController> logger, 
        IDeactivateAccessCardsService deactivateAccessCardsService) : base(logger)
    {
        _deactivateAccessCardsService = deactivateAccessCardsService;
    }


    [Produces<DeactivateAccessCardResultVm>]
    [HttpPost]
    public IActionResult Post([FromBody] AccessCardsToDeactivateIm accessCardsToDeactivate)
    {
        return ReturnOkResult(() => _deactivateAccessCardsService.Deactivate(accessCardsToDeactivate));
    }
}