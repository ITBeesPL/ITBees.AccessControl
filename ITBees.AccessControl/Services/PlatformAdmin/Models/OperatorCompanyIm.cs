namespace ITBees.AccessControl.Services.PlatformAdmin.Models;

public class OperatorCompanyIm
{
    public string CompanyName { get; set; }
    public bool IsActive { get; set; }
    public string? CompanyShortName { get; set; }
    public string? Street { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? Nip { get; set; }
}