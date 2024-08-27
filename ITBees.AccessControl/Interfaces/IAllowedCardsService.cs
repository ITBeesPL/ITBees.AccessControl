using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.AccessControl.Services;

namespace ITBees.AccessControl.Interfaces;

public interface IAllowedCardsService
{
    AllowedAccessCardsVm RegisterCard(AllowedAccessCardsIm allowedAccessCardIm);
    bool IsCardAllowedToAuthorize(string cardId);
}