using ITBees.AccessControl.Interfaces.ViewModels;

namespace ITBees.AccessControl.Interfaces;

public interface IAllowedCardsService
{
    AllowedAccessCardVm RegisterCard(AllowedAccessCardIm allowedAccessCardIm);
    bool IsCardAllowedToAuthorize(string cardId);
}