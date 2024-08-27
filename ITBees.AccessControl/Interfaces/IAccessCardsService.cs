using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IAccessCardsService
{
    PaginatedResult<AccessCardVm> GetMyCompanyCards(int page, int pageSize, string sortColumn, SortOrder sortOrder);
}