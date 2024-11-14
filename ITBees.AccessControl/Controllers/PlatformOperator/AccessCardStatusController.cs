using ITBees.AccessControl.Controllers.PlatformOperator.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformOperator;

[Authorize]
public class AccessCardStatusController : RestfulControllerBase<AccessCardStatusController>
{
    private readonly IAccessCardStatusService _accessCardStatusService;

    public AccessCardStatusController(ILogger<AccessCardStatusController> logger,
        IAccessCardStatusService accessCardStatusService) : base(logger)
    {
        _accessCardStatusService = accessCardStatusService;
    }

    [HttpGet]
    [Produces<AccessCardStatusVm>]
    public IActionResult Get(string cardId)
    {
        return ReturnOkResult(() => _accessCardStatusService.Check(cardId));
    }
}