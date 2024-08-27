using ITBees.AccessControl.Interfaces;
using ITBees.Interfaces.Repository;
using ITBees.Models.Roles;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers;

[Authorize(Roles = Role.PlatformOperator)]
public class UnauthorizedAccessCardLogController : RestfulControllerBase<UnauthorizedAccessCardLogController>
{
    private readonly IUnauthorizedAccessCardLogsService _unauthorizedAccessCardLogsService;

    public UnauthorizedAccessCardLogController(
        ILogger<UnauthorizedAccessCardLogController> logger,
        IUnauthorizedAccessCardLogsService unauthorizedAccessCardLogsService) : base(logger)
    {
        _unauthorizedAccessCardLogsService = unauthorizedAccessCardLogsService;
    }

    [HttpGet]
    public IActionResult Get(int page, int pageSize, string sortColumn, SortOrder sortOrder)
    {
        return ReturnOkResult(()=>_unauthorizedAccessCardLogsService.GetLogs(page, pageSize, sortColumn, sortOrder));
    }

}