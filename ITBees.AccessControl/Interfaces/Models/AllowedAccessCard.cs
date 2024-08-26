using ITBees.Models.Users;

namespace ITBees.AccessControl.Interfaces.Models;

public class AllowedAccessCard
{
    public Guid Guid { get; set; }
    public string CardId { get; set; }
    public UserAccount CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
}