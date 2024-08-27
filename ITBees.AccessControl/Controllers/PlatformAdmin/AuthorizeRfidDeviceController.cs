using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class AuthorizeRfidDeviceController : RestfulControllerBase<AuthorizeRfidDeviceController>
{
    private readonly IAuthorizeRfidDeviceService _authorizeRfidDeviceService;

    public AuthorizeRfidDeviceController(
        ILogger<AuthorizeRfidDeviceController> logger,
        IAuthorizeRfidDeviceService authorizeRfidDeviceService) : base(logger)
    {
        _authorizeRfidDeviceService = authorizeRfidDeviceService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] AuthorizeRfidDeviceIm authorizeRfidDeviceIm)
    {
        return ReturnOkResult(() => _authorizeRfidDeviceService.Authorize(authorizeRfidDeviceIm));
    }
}