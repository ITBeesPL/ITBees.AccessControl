using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services;

class UnauthorizedAccessCardLogsService : IUnauthorizedAccessCardLogsService
{
    private readonly IReadOnlyRepository<UnauthorizedAccessCardLog> _unauthorizedAccessCardLogRoRepo;
    private readonly IAspCurrentUserService _aspCurrentUserService;

    public UnauthorizedAccessCardLogsService(
        IReadOnlyRepository<UnauthorizedAccessCardLog> unauthorizedAccessCardLogRoRepo,
        IAspCurrentUserService aspCurrentUserService)
    {
        _unauthorizedAccessCardLogRoRepo = unauthorizedAccessCardLogRoRepo;
        _aspCurrentUserService = aspCurrentUserService;
    }
    public PaginatedResult<UnauthorizedAccessCardLogVm> GetLogs(int page, int pageSize, string sortCoulum, SortOrder sortOrder)
    {
        if (_aspCurrentUserService.CurrentUserIsPlatformOperator() == null)
        {
            throw new UnauthorizedAccessException("You are not allowed to access this logs");
        }

        var result = _unauthorizedAccessCardLogRoRepo.GetDataPaginated(x => true, page, pageSize, sortCoulum, sortOrder, x => x.LastConnectedTroughRfidReaderDevice);
        
        return new PaginatedResult<UnauthorizedAccessCardLogVm>()
        {
            AllElementsCount = result.AllElementsCount,
            AllPagesCount = result.AllPagesCount,
            CurrentPage = result.CurrentPage,
            Data = result.Data.Select(x => new UnauthorizedAccessCardLogVm(x)).ToList()
        };
    }
}