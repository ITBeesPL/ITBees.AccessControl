using ITBees.AccessControl.Controllers.PlatformOperator;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Services.PlatformOperator;

public interface IAcGroupAccessCardsService
{
    PaginatedResult<AcGroupAccessCardsVm> GetAll(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder);
}