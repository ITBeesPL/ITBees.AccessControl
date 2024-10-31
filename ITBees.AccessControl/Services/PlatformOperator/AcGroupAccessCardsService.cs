using ITBees.AccessControl.Controllers.PlatformOperator;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.Interfaces.Repository;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services.PlatformOperator;

public class AcGroupAccessCardsService : IAcGroupAccessCardsService
{
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IReadOnlyRepository<AcGroupAccessCards> _accessCardGroupRoRepo;

    public AcGroupAccessCardsService(IAspCurrentUserService aspCurrentUserService, IReadOnlyRepository<AcGroupAccessCards> accessCardGroupRoRepo)
    {
        _aspCurrentUserService = aspCurrentUserService;
        _accessCardGroupRoRepo = accessCardGroupRoRepo;
    }
    public PaginatedResult<AcGroupAccessCardsVm> GetAll(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        return _accessCardGroupRoRepo.GetDataPaginated(x => true, new SortOptions(page, pageSize, sortColumn, sortOrder)).MapTo(x => new AcGroupAccessCardsVm(x));
    }
}