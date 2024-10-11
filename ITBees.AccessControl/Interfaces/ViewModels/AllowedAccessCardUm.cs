namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AllowedAccessCardUm
{
    public Guid Guid { get; set; }
    public string CardId { get; set; }
    public int AccessCardTypeId { get; set; }
    public Guid? CompanyGuid { get; set; }
}