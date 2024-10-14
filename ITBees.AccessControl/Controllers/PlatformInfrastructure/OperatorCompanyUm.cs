namespace ITBees.AccessControl.Controllers.PlatformInfrastructure;

public class OperatorCompanyUm
{
    public Guid Guid { get; set; }
    public string City { get; set; }
    public string CompanyName { get; set; }
    public string CompanyShortName { get; set; }
    public bool IsActive { get; set; }
    public  string Nip { get; set; }
    public Guid? OwnerGuid { get; set; }
    public string PostCode { get; set; }
    public string Street { get; set; }
}