using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware.Ac;
using ITBees.UserManager.Controllers.Models;

namespace ITBees.AccessControl.Services.PlatformOperator;

public interface IAcGroupsService
{
    AcGroupVm Get(Guid guid);
    AcGroupVm Update(AcGroupUm acGroupUm);
    AcGroupVm Create(AcGroupIm acGroupIm);
    DeleteResultVm Delete(AcGroupDm acGroupDm);
    PaginatedResult<AcGroupVm> GetAll(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder);
}