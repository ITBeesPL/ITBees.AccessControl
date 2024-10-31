using ITBees.AccessControl.Controllers.PlatformInfrastructure;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AccessCardVm
{
    public Guid Guid { get; set; }
    public string  CardId { get; set; }
    public DateTime? LastUsedDate { get; set; }
    public string OwnerName { get; set; }
    public string OwnerEmail { get; set; }
    public string AccessCardType { get; set; }
    public bool InvitationSend { get; set; }
    public List<AccessCardGroupVm> AccessCardGroups{ get; set; }
    public DateTime ValidTo { get; set; }
}