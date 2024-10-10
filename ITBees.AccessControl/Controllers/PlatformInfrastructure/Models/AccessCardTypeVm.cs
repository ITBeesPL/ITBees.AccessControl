using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Controllers.PlatformInfrastructure.Models;

public class AccessCardTypeVm
{
    public AccessCardTypeVm()
    {
        
    }
    public AccessCardTypeVm(AccessCardType x)
    {
        Id = x.Id;
        Name = x.Name;
        IsActive = x.IsActive;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}