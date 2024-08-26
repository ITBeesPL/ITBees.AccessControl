using ITBees.AccessControl.Interfaces.Models;
using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AllowedAccessCardVm
{
    public AllowedAccessCardVm()
    {
        
    }

    public AllowedAccessCardVm(AllowedAccessCard x)
    {
        CardId = x.CardId;
        Created = x.Created;
        CreatedByGuid = x.CreatedByGuid;
        CreatedBy = x.CreatedBy?.DisplayName;
        Guid = x.Guid;
    }

    public AllowedAccessCardVm(AccessCard x)
    {
        CardId = x.CardId;
        Created = x.Created;
        CreatedByGuid = x.CreatedByGuid;
        CreatedBy = x.CreatedBy?.DisplayName;
        Guid = x.Guid;
    }

    public Guid Guid { get; set; }
    public string CardId { get; set; }
    public string CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
}