using ITBees.AccessControl.Interfaces.Models;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AcGroupVm
{
    public AcGroupVm(AcGroup x)
    {
        Guid = x.Guid;
        GroupName = x.GroupName;
        GroupDescription = x.GroupDescription;
        CreatedBy = x.CreatedBy?.DisplayName;
        Company = x.Company.CompanyName;
        IsActive = x.IsActive;
    }

    public AcGroupVm()
    {
        
    }

    public Guid Guid { get; set; }
    public String GroupName { get; set; }
    public String GroupDescription { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public string Company { get; set; }
    public bool IsActive { get; set; }
}