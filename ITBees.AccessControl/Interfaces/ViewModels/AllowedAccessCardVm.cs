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
        CompanyGuid = x.CompanyGuid;
        CompanyName = x.Company?.CompanyName;
        ActivationDate= x.ActivationDate;
        AccessCardTypeId = x.AccessCardTypeId;
        IsActive = x.IsActive;
    }

    public AllowedAccessCardVm(AccessCard x)
    {
        CardId = x.CardId;
        Created = x.Created;
        CreatedByGuid = x.CreatedByGuid;
        CreatedBy = x.CreatedBy?.DisplayName;
        Guid = x.Guid;
        IsActive = x.IsActive;
        
    }

    public Guid Guid { get; set; }
    public string CardId { get; set; }
    public string CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public int AccessCardTypeId { get; set; }
    public Guid? CompanyGuid { get; set; }
    public string? CompanyName { get; set; }
    public DateTime? ActivationDate { get; set; }
}