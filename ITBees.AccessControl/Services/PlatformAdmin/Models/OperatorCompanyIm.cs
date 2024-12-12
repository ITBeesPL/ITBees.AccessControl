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
    public string? BuildingStreet { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public string? EmployeeEmail { get; set; }
    public bool EmployeeSendInvitation { get; set; }
    public Guid? UserRoleGuid { get; set; }
    public string? NetworkIpAddress { get; set; }
    public string? NetworkMask { get; set; }
    public string? NetworkGateway { get; set; }
    public string? NetworkDns { get; set; }
    public List<string>? Zones { get; set; }
}