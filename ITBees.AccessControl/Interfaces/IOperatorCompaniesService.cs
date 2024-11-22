using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Controllers.PlatformInfrastructure;
using ITBees.AccessControl.Services.PlatformAdmin.Models;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IOperatorCompaniesService
{
    PaginatedResult<OperatorCompanyVm> Get(string search, int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder);
    OperatorCompanyVm Get(Guid guid);
    Task<OperatorCompanyVm> Create(OperatorCompanyIm x);
    OperatorCompanyVm UpdateCompany(OperatorCompanyUm operatorCompanyUm);
}