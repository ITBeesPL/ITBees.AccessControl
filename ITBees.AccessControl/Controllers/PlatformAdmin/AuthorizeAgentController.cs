using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class AuthorizeAgentController : RestfulControllerBase<AuthorizeAgentController>
{
    private readonly IAuthorizeRfidDeviceService _authorizeRfidDeviceService;

    public AuthorizeAgentController(
        ILogger<AuthorizeAgentController> logger,
        IAuthorizeRfidDeviceService authorizeRfidDeviceService) : base(logger)
    {
        _authorizeRfidDeviceService = authorizeRfidDeviceService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] AuthorizeDeviceIm authorizeDeviceIm)
    {
        return ReturnOkResult(() => _authorizeRfidDeviceService.Authorize(authorizeDeviceIm));
    }
}