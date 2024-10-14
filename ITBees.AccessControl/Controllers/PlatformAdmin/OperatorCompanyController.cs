using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Services.PlatformAdmin.Models;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class OperatorCompanyController : RestfulControllerBase<OperatorCompanyController>
{
    private readonly IOperatorCompaniesService _operatorCompaniesService;

    public OperatorCompanyController(ILogger<OperatorCompanyController> logger,
        IOperatorCompaniesService operatorCompaniesService) : base(logger)
    {
        _operatorCompaniesService = operatorCompaniesService;
    }

    [HttpGet]
    [Produces<OperatorCompanyVm>]
    public IActionResult Get(Guid guid)
    {
        return ReturnOkResult(() => _operatorCompaniesService.Get(guid));
    }

    [HttpPost]
    [Produces<OperatorCompanyVm>]
    public IActionResult Post([FromBody] OperatorCompanyIm operatorCompanyIm)
    {
        return ReturnOkResult(() => _operatorCompaniesService.Create(operatorCompanyIm));
    }
}