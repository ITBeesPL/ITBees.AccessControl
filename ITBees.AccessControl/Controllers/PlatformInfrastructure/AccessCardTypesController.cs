using ITBees.AccessControl.Controllers.PlatformInfrastructure.Models;
using ITBees.AccessControl.Services.Common;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformInfrastructure;

public class AccessCardTypesController : RestfulControllerBase<AccessCardTypesController>
{
    private readonly IAccessCardTypesService _accessCardTypesService;

    public AccessCardTypesController(ILogger<AccessCardTypesController> logger, IAccessCardTypesService accessCardTypesService) : base(logger)
    {
        _accessCardTypesService = accessCardTypesService;
    }

    [HttpGet]
    [Produces<List<AccessCardTypeVm>>]
    public IActionResult Get()
    {
        return ReturnOkResult(() => _accessCardTypesService.Get());
    }
}