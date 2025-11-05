using ITBees.Interfaces.Repository;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services.Audit;

public class AuditLogService : IAuditLogService
{
    private readonly IAspCurrentUserService _currentUserService;
    private readonly IWriteOnlyRepository<AuditLog> _auditLogRwRepo;

    public AuditLogService(IAspCurrentUserService currentUserService, IWriteOnlyRepository<AuditLog> auditLogRwRepo)
    {
        _currentUserService = currentUserService;
        _auditLogRwRepo = auditLogRwRepo;
    }

    public int SaveAuditLog(Guid? userAccountGuid,Guid? companyGuid, int auditLogTypeId, string action, string description,
        string entityName, int? entityId,
        Guid? entityGuid)
    {
       var newAuditLog = _auditLogRwRepo.InsertData(new AuditLog()
        {
            UserAccountGuid = userAccountGuid,
            Action = action,
            Description = description,
            EntityName = entityName,
            EntityId = entityId,
            EntityGuid = entityGuid,
            AuditLogTypeId = auditLogTypeId,
            DateTime = DateTime.UtcNow,
            CompanyGuid = companyGuid,
        });

        return newAuditLog.Id;
    }
}