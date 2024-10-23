using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Controllers.PlatformInfrastructure;
using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Services.PlatformAdmin.Models;
using ITBees.FAS.SatelliteAgents.Database;
using ITBees.Interfaces.Repository;
using ITBees.Models.Buildings;
using ITBees.Models.Companies;
using ITBees.Models.Hardware;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services.PlatformAdmin;

public class OperatorCompaniesService : IOperatorCompaniesService
{
    private readonly IReadOnlyRepository<Company> _companyRoRepo;
    private readonly IWriteOnlyRepository<Company> _companyRwRepo;
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IReadOnlyRepository<Building> _buildingRoRepo;
    private readonly IReadOnlyRepository<PhysicalDeviceHub> _devicesRoRepo;

    public OperatorCompaniesService(
        IReadOnlyRepository<Company> companyRoRepo,
        IWriteOnlyRepository<Company> companyRwRepo,
        IAspCurrentUserService aspCurrentUserService,
        IReadOnlyRepository<Building> buildingRoRepo,
        IReadOnlyRepository<PhysicalDeviceHub> devicesRoRepo)
    {
        _companyRoRepo = companyRoRepo;
        _companyRwRepo = companyRwRepo;
        _aspCurrentUserService = aspCurrentUserService;
        _buildingRoRepo = buildingRoRepo;
        _devicesRoRepo = devicesRoRepo;
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

        if (sortOrder == null)
            sortOrder = SortOrder.Ascending;

        if (!string.IsNullOrEmpty(search))
        {
            var result = _companyRoRepo.GetDataPaginated(x => x.CompanyName.Contains(search) || x.CompanyShortName.Contains(search) || x.Owner.Email.Contains(search),
                page.Value,
                pageSize.Value,
                sortColumn,
                sortOrder.Value,
                x => x.Owner);
            var resultDevicesAndBuildings = GetInfrastructureData(result);
            return GetCompanyWithCounters(result, resultDevicesAndBuildings);
        }

        var resultPaginated = _companyRoRepo.GetDataPaginated(x => true,
            page.Value,
            pageSize.Value,
            sortColumn,
            sortOrder.Value,
            x => x.Owner, x=>x.CreatedBy);
        var resultDevicesAndBuildings2 = GetInfrastructureData(resultPaginated);
        return GetCompanyWithCounters(resultPaginated, resultDevicesAndBuildings2);
    }

    private static PaginatedResult<OperatorCompanyVm> GetCompanyWithCounters(PaginatedResult<Company> result,
        InfrastructureElements resultDevicesAndBuildings)
    {
        return result.MapTo(x => new OperatorCompanyVm(x,
            resultDevicesAndBuildings.Builidngs.Count(y => y.CompanyGuid == x.Guid),
            resultDevicesAndBuildings.Devices.Count(y => y.CompanyGuid == x.Guid)));
    }

    private InfrastructureElements GetInfrastructureData(PaginatedResult<Company> companies)
    {
        var companyGuids = companies.Data.Select(x => x.Guid);
        var buildings = _buildingRoRepo.GetData(x => companyGuids.Contains(x.CompanyGuid.Value));
        var devices = _devicesRoRepo.GetData(x => companyGuids.Contains(x.CompanyGuid.Value));
        return new InfrastructureElements() { Builidngs = buildings, Devices = devices };
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
        }, x => x.CreatedBy).First();

        return new OperatorCompanyVm(result);
    }
}

internal class InfrastructureElements
{
    public ICollection<Building> Builidngs { get; set; }
    public ICollection<PhysicalDeviceHub> Devices { get; set; }
}