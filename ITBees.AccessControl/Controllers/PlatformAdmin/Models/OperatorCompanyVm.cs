using ITBees.Models.Companies;

namespace ITBees.AccessControl.Controllers.PlatformAdmin.Models;

public class OperatorCompanyVm
{
    public OperatorCompanyVm()
    {
        
    }
    public OperatorCompanyVm(Company x)
    {
        Guid = x.Guid;
        Name = x.CompanyName;
        Email = x.Owner?.Email;
        OwnerName = x.Owner?.DisplayName;
    }

    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string OwnerName { get; set; }
}