using ITBees.AccessControl.Interfaces.Models;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AllowedAccessCardsVm
{
    public AllowedAccessCardsVm(List<AllowedAccessCard> x)
    {
        this.AllowedAccessCards = x;
    }

    public List<AllowedAccessCard> AllowedAccessCards { get; set; }
}