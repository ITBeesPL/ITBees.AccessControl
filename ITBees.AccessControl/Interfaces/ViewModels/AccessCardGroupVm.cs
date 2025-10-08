using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AccessCardGroupVm
{
    public AccessCardGroupVm(AcGroup x)
    {
        Guid = x.Guid;
        Name = x.GroupName;
        Description = x.GroupDescription;
        Created = x.Created;
    }

    public AccessCardGroupVm()
    {
        
    }

    public string Description { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; } 
}