using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Controllers.PlatformInfrastructure;
using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Services.PlatformAdmin.Models;
using ITBees.Interfaces.Repository;
using ITBees.Models.Companies;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services.PlatformAdmin;

public class OperatorCompaniesService : IOperatorCompaniesService
{
    private readonly IReadOnlyRepository<Company> _companyRoRepo;
    private readonly IWriteOnlyRepository<Company> _companyRwRepo;
    private readonly IAspCurrentUserService _aspCurrentUserService;

    public OperatorCompaniesService(
        IReadOnlyRepository<Company> companyRoRepo,
        IWriteOnlyRepository<Company> companyRwRepo,
        IAspCurrentUserService aspCurrentUserService)
    {
        _companyRoRepo = companyRoRepo;
        _companyRwRepo = companyRwRepo;
        _aspCurrentUserService = aspCurrentUserService;
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
            return result.MapTo(x => new OperatorCompanyVm(x));
        }

        var resultPaginated = _companyRoRepo.GetDataPaginated(x => true,
            page.Value,
            pageSize.Value,
            sortColumn,
            sortOrder.Value,
            x => x.Owner);
        return resultPaginated.MapTo(x => new OperatorCompanyVm(x));
    }

    public OperatorCompanyVm Get(Guid guid)
    {
        return _companyRoRepo.GetData(x => x.Guid == guid).Select(x => new OperatorCompanyVm(x)).First();
    }

    public OperatorCompanyVm Create(OperatorCompanyIm x)
    {
        var company = _companyRwRepo.InsertData(new Company()
        {
            City = x.City,
            CompanyName = x.CompanyName,
            CompanyShortName = x.CompanyShortName,
            Created = DateTime.Now,
            CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid(),
            IsActive = x.IsActive,
            PostCode = x.PostCode,
            Nip = x.Nip,
            Street = x.Street
        });

        var createdCompany = _companyRoRepo.GetData(x => x.Guid == company.Guid, x => x.CreatedBy).First();
        return new OperatorCompanyVm(createdCompany);
    }

    public OperatorCompanyVm UpdateCompany(OperatorCompanyUm operatorCompanyUm)
    {
        var result = _companyRwRepo.UpdateData(x => x.Guid == operatorCompanyUm.Guid, x =>
        {
            x.City = operatorCompanyUm.City;
            x.CompanyName = operatorCompanyUm.CompanyName;
            x.CompanyShortName = operatorCompanyUm.CompanyShortName;
            x.IsActive = operatorCompanyUm.IsActive;
            x.Nip = operatorCompanyUm.Nip;
            x.OwnerGuid = operatorCompanyUm.OwnerGuid;
            x.PostCode = operatorCompanyUm.PostCode;
            x.Street = operatorCompanyUm.Street;
        }, x=>x.CreatedBy).First();

        return new OperatorCompanyVm(result);
    }
}