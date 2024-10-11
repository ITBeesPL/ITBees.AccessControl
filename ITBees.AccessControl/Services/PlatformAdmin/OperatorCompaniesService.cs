using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.Interfaces.Repository;
using ITBees.Models.Companies;

namespace ITBees.AccessControl.Services.PlatformAdmin;

public class OperatorCompaniesService : IOperatorCompaniesService
{
    private readonly IReadOnlyRepository<Company> _companyRoRepo;

    public OperatorCompaniesService(IReadOnlyRepository<Company> companyRoRepo)
    {
        _companyRoRepo = companyRoRepo;
    }

    public PaginatedResult<OperatorCompanyVm> Get(string? search, int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        if (page == null)
        {
            page = 1;
        }

        if (pageSize == null)
        {
            pageSize = 1000;
        }

        if (sortColumn == null)
        {
            sortColumn = "";
        }
        if (!string.IsNullOrEmpty(search))
        {
            var result = _companyRoRepo.GetDataPaginated(x => x.CompanyName.Contains(search) || x.CompanyShortName.Contains(search) || x.Owner.Email.Contains(search), 
                page.Value, 
                pageSize.Value, 
                sortColumn, 
                sortOrder.Value, 
                x => x.Owner);
            return result.MapTo(x=>new OperatorCompanyVm(x));
        }

        var resultPaginated = _companyRoRepo.GetDataPaginated(x => true, 
            page.Value, 
            pageSize.Value, 
            sortColumn, 
            sortOrder.Value, 
            x=>x.Owner);
        return resultPaginated.MapTo(x => new OperatorCompanyVm(x));
    }
}