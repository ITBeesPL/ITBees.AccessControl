using ITBees.AccessControl.Interfaces.Models;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AllowedAccessCardsVm
{
    public AllowedAccessCardsVm(List<AllowedAccessCard> x)
    {
        this.AllowedAccessCards = x.Select(y=>new AllowedAccessCardVm(y)).ToList();
    }

    public List<AllowedAccessCardVm> AllowedAccessCards { get; set; }
}