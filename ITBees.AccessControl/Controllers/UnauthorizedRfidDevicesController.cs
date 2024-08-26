using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers;

[Authorize(Roles = Role.PlatformOperator)]
public class UnauthorizedRfidDevicesController : RestfulControllerBase<UnauthorizedRfidDevicesController>
{
    private readonly IUnauthorizedRfidDevicesService _unauthorizedRfidDevicesService;

    public UnauthorizedRfidDevicesController(
        ILogger<UnauthorizedRfidDevicesController> logger,
        IUnauthorizedRfidDevicesService unauthorizedRfidDevicesService) : base(logger)
    {
        _unauthorizedRfidDevicesService = unauthorizedRfidDevicesService;
    }

    [HttpGet]
    [Produces<List<UnauthorizedRfidDeviceVm>>]
    public IActionResult Get(int page, int pageSize, string sortCoulum, SortOrder sortOrder)
    {
        return ReturnOkResult(() => _unauthorizedRfidDevicesService.Get());
    }
}