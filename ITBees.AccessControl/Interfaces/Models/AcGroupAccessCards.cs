using ITBees.Models.Hardware;
using ITBees.Models.Users;

namespace ITBees.AccessControl.Interfaces.Models;

public class AcGroupAccessCards
{
    public AccessCard AccessCard { get; set; }
    public Guid AccessCardGuid { get; set; }
    public AcGroup AcGroup { get; set; }
    public Guid AcGroupGuid { get; set; }
    public UserAccount CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
}