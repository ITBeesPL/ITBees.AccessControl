using ITBees.AccessControl.Controllers.PlatformAdmin;
using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.AccessControl.Services;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IAllowedCardsService
{
    AllowedAccessCardsVm RegisterCard(AllowedAccessCardsIm allowedAccessCardIm);
    IsCardAllowedResult IsCardAllowedToAuthorize(string cardId);
    PaginatedResult<AllowedAccessCardVm> GetCards(int page, int pageSize, string sortColumn, SortOrder sortOrder);
    DeleteAccessCardResultVm Delete(AllowedAccessCardsDm allowedAccessCardsDm);
    void SetCardAsActive(Guid? cardGuid);
    AllowedAccessCardVm UpdateCard(AllowedAccessCardUm allowedAccessCardUm);
}