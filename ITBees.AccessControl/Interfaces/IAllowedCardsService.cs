using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.AccessControl.Services;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IAllowedCardsService
{
    AllowedAccessCardsVm RegisterCard(AllowedAccessCardsIm allowedAccessCardIm);
    bool IsCardAllowedToAuthorize(string cardId);
    PaginatedResult<AllowedAccessCardVm> GetCards(int page, int pageSize, string sortColumn, SortOrder sortOrder);
}