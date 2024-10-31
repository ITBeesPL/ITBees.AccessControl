using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Interfaces.Models;

public interface IAcGroupAccessCards
{
    public AccessCard AccessCard { get; set; }
    public Guid AccessCardGuid { get; set; }
    public AcGroup AcGroup { get; set; }
    public Guid AcGroupGuid { get; set; }
}