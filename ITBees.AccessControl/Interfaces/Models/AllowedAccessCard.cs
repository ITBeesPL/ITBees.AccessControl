using ITBees.Models.Companies;
using ITBees.Models.Users;

namespace ITBees.AccessControl.Interfaces.Models;

public class AllowedAccessCard
{
    public Guid Guid { get; set; }
    public string CardId { get; set; }
    public UserAccount CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
    public Guid? CompanyGuid { get; set; }
    public Company Company { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ActivationDate { get; set; }
    public int AccessCardTypeId { get; set; }
}