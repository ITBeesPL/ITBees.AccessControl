using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.AccessControl.Services.PlatformOperator;
using ITBees.RestfulApiControllers;
using ITBees.UserManager.Controllers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformOperator;

[Authorize]
public class AcGroupController : RestfulControllerBase<AcGroupController>
{
    private readonly IAcGroupsService _acGroupsService;

    public AcGroupController(ILogger<AcGroupController> logger, IAcGroupsService acGroupsService) : base(logger)
    {
        _acGroupsService = acGroupsService;
    }

    [HttpGet]
    [Produces<AcGroupVm>]
    public IActionResult Get(Guid guid)
    {
        return ReturnOkResult(() => _acGroupsService.Get(guid));
    }

    [HttpPost]
    [Produces<AcGroupVm>]
    public IActionResult Post([FromBody] AcGroupIm acGroupIm)
    {
        return ReturnOkResult(() => _acGroupsService.Create(acGroupIm));
    }

    [HttpPut]
    [Produces<AcGroupVm>]
    public IActionResult Post([FromBody] AcGroupUm acGroupUm)
    {
        return ReturnOkResult(() => _acGroupsService.Update(acGroupUm));
    }

    [HttpDelete]
    [Produces<DeleteResultVm>]
    public IActionResult Del([FromBody] AcGroupDm acGroupDm)
    {
        return ReturnOkResult(() => _acGroupsService.Delete(acGroupDm));
    }
}