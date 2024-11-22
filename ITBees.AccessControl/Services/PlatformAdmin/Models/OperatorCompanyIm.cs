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
    public string? CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }

    public string? BuildingName { get; set; }
    public string? BuildingPostCode { get; set; }
    public string? BuildingCity { get; set; }
    public string? BuilidingStreet { get; set; }
    public string? EmployeFirstName { get; set; }
    public string? EmployeLastName { get; set; }
    public bool EmployeeSendInvitation { get; set; }

    public string? NetworkIpAddress { get; set; }
    public string? NetworkMask { get; set; }
    public string? NetworkGateway { get; set; }
    public string? NetworkDns { get; set; }
    public string? EmployeEmail { get; set; }
}