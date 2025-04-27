namespace ITBees.AccessControl.Services.Audit;

public interface IAuditLogService
{
    void SaveAuditLog(Guid? userAccountGuid, Guid? companyGuid, int auditLogTypeId, string action, string description, string entityName,
        int entityId, Guid entityGuid);
}