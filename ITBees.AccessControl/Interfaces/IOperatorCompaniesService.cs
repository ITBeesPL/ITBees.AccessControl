using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IOperatorCompaniesService
{
    PaginatedResult<OperatorCompanyVm> Get(string search, int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder);
}