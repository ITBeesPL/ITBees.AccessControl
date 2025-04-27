using ITBees.Models.Companies;
using ITBees.Models.Users;

namespace ITBees.AccessControl.Services.Audit;

public class AuditLog
{
    public int Id { get; set; }
    public AuditLogType AuditLogType { get; set; }
    public int AuditLogTypeId { get; set; }
    public DateTime DateTime { get; set; }
    public UserAccount UserAccount { get; set; }
    public Guid? UserAccountGuid { get; set; }
    public string Action { get; set; }
    public string? Description { get; set; }
    public string? EntityName { get; set; }
    public int? EntityId { get; set; }
    public Guid? EntityGuid { get; set; }
    public Company Company { get; set; }
    public Guid? CompanyGuid { get; set; }
}