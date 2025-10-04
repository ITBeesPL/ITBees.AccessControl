using ITBees.AccessControl.Controllers.PlatformInfrastructure;
using ITBees.RestClient.Interfaces.RestModelMarkup;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AccessCardVm : Vm
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
    public DateTime Created { get; set; }
    public DateTime Deactivated { get; set; }
    public bool IsActive { get; set; }
}