using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Models.Companies;
using ITBees.Models.Users;

namespace ITBees.AccessControl.Interfaces.Models;

public class AcGroup
{
    public AcGroup()
    {
        
    }
    public AcGroup(AcGroupIm x, Guid createdBy)
    {
        GroupDescription = x.GroupDescription;
        GroupName = x.GroupName;
        Created = DateTime.Now;
        CreatedByGuid = createdBy;
        CompanyGuid = x.CompanyGuid;
        IsActive = x.IsActive;
    }

    public Guid Guid { get; set; }
    public String GroupName { get; set; }
    public String GroupDescription { get; set; }
    public DateTime Created { get; set; }
    public UserAccount CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public Company Company { get; set; }
    public Guid CompanyGuid { get; set; }
    public bool IsActive { get; set; }
}