using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.Interfaces.Repository;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformAdmin;

[Authorize(Roles = Role.PlatformOperator)]
public class OperatorCompaniesController: RestfulControllerBase<OperatorCompaniesController>
{
    private readonly IOperatorCompaniesService _operatorCompaniesService;

    public OperatorCompaniesController(ILogger<OperatorCompaniesController> logger,
        IOperatorCompaniesService operatorCompaniesService) : base(logger)
    {
        _operatorCompaniesService = operatorCompaniesService;
    }

    [HttpGet]
    [Produces<PaginatedResult<OperatorCompanyVm>>]
    public IActionResult Get(string? search, int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        return ReturnOkResult(() => _operatorCompaniesService.Get(search, page, pageSize, sortColumn, sortOrder));
    }
}