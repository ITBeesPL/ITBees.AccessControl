using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.Models.Users;
using ITBees.RestfulApiControllers.Exceptions;
using ITBees.RestfulApiControllers.Models;
using ITBees.UserManager.Controllers.Models;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services.PlatformOperator;

public class AcGroupsService : IAcGroupsService
{
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IWriteOnlyRepository<AcGroup> _acGroupRwRepo;
    private readonly IReadOnlyRepository<AcGroup> _acGroupRoRepo;

    public AcGroupsService(IAspCurrentUserService aspCurrentUserService,
        IWriteOnlyRepository<AcGroup> acGroupRwRepo,
        IReadOnlyRepository<AcGroup> acGroupRoRepo
    )
    {
        _aspCurrentUserService = aspCurrentUserService;
        _acGroupRwRepo = acGroupRwRepo;
        _acGroupRoRepo = acGroupRoRepo;
    }

    public AcGroupVm Get(Guid guid)
    {
        return GetAcGroupVm(guid);
    }

    public AcGroupVm Update(AcGroupUm acGroupUm)
    {
        if (_aspCurrentUserService.TryCanIDoForCompany(TypeOfOperation.Rw,
                _aspCurrentUserService.GetCurrentUser().LastUsedCompanyGuid))
        {
            var updated = _acGroupRwRepo.UpdateData(x => x.Guid == acGroupUm.Guid, x => { });
            return GetAcGroupVm(acGroupUm.Guid);
        }

        throw new FasApiErrorException(new FasApiErrorVm("You have no rights to change groups", 401, ""));
    }

    public AcGroupVm Create(AcGroupIm acGroupIm)
    {
        var result = _acGroupRwRepo.InsertData(new AcGroup(acGroupIm, _aspCurrentUserService.GetCurrentUserGuid().Value));
        if (_aspCurrentUserService.TryCanIDoForCompany(TypeOfOperation.Rw,
                _aspCurrentUserService.GetCurrentUser().LastUsedCompanyGuid))
        {
            return GetAcGroupVm(result.Guid);
        };

        throw new FasApiErrorException(new FasApiErrorVm("You have no rights to add groups", 401, ""));
    }

    private AcGroupVm GetAcGroupVm(Guid resultGuid)
    {
        return _acGroupRoRepo.GetData(x => x.Guid == resultGuid, x => x.Company, x => x.CreatedBy).Select(x => new AcGroupVm(x)).First();
    }

    public DeleteResultVm Delete(AcGroupDm acGroupDm)
    {
        var result = _acGroupRwRepo.DeleteData(x => x.Guid == acGroupDm.Guid);

        if (result != 1)
            throw new FasApiErrorException(new FasApiErrorVm("Could not delete group", 400, ""));

        return new DeleteResultVm() { Message = "", Success = true };
    }

    public PaginatedResult<AcGroupVm> GetAll(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        var cu = _aspCurrentUserService.GetCurrentSessionUser().CurrentUser.LastUsedCompanyGuid;
        return _acGroupRoRepo.GetDataPaginated(x => x.CompanyGuid == cu, new SortOptions(page, pageSize, sortColumn, sortOrder), x=>x.Company, x=>x.CreatedBy)
            .MapTo(x => new AcGroupVm(x));
    }
}