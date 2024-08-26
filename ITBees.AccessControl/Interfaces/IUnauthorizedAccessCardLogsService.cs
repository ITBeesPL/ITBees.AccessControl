using ITBees.AccessControl.Controllers;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IUnauthorizedAccessCardLogsService
{
    PaginatedResult<UnauthorizedAccessCardLogVm> GetLogs(int page, int pageSize, string sortCoulum, SortOrder sortOrder);
}