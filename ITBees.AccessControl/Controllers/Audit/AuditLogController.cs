using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.Audit;

[Authorize]
public class AuditLogController : RestfulControllerBase<AuditLogController>
{
    public AuditLogController(ILogger<AuditLogController> logger) : base(logger)
    {
    }
}