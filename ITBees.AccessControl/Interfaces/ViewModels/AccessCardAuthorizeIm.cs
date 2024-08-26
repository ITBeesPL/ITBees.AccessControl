using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AccessCardAuthorizeIm
{
    public string CardId { get; set; }
    public AccessCardType AccessCardType { get; set; }
    public DateTime ValidDate { get; set; }
    public string OwnerName { get; set; }
    public string OwnerEmail { get; set; }
    public bool IsActive { get; set; }
    public bool InvitationSend { get; set; }
    public Guid CompanyGuid { get; set; }
}