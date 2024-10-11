namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AllowedAccessCardIm
{
    public AllowedAccessCardIm()
    {

    }

    public AllowedAccessCardIm(AccessCardAuthorizeIm x)
    {
        CardId = x.CardId;
    }

    public string CardId { get; set; }
    public int AccessCardTypeId { get; set; }
    public Guid? CompanyGuid { get; set; }
}