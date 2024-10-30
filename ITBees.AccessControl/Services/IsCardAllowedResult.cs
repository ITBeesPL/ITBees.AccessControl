namespace ITBees.AccessControl.Services;

public class IsCardAllowedResult
{
    public bool Allowed { get; set; }
    public Guid? CardGuid { get; set; }
    public int AccessCardTypeId { get; set; }
}