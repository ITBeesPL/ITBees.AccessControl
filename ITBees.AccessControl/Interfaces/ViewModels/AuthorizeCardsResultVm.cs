namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AuthorizeCardsResultVm
{
    public List<AllowedAccessCardVm> AllowedAccessCards { get; set; } = new();
    public List<string> UnauthorizedCards { get; set; } = new();
}