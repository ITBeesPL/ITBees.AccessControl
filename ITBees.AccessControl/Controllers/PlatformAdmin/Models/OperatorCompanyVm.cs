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
        OwnerGuid = x.OwnerGuid;
        IsActive = x.IsActive;
        City = x.City;
        CompanyShortName = x.CompanyShortName;
        Nip = x.Nip;
        PostCode = x.PostCode;
        Street = x.Street;
        DevicesCount = 1;
        BuildingsCount = 0;
    }

    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string OwnerName { get; set; }

    public string City { get; set; }
    public string CompanyName { get; set; }
    public string CompanyShortName { get; set; }
    public bool IsActive { get; set; }
    public string Nip { get; set; }
    public Guid? OwnerGuid { get; set; }
    public string PostCode { get; set; }
    public string Street { get; set; }
    public int BuildingsCount { get; set; }
    public int DevicesCount { get; set; }
}