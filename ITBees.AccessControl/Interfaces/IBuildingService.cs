using ITBees.AccessControl.Controllers.PlatformAdmin;
using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.Interfaces.Repository;

namespace ITBees.AccessControl.Interfaces;

public interface IBuildingService
{
    BuildingVm Get(Guid guid);
    BuildingVm Create(BuildingIm buildingIm);
    BuildingVm Update(BuildingUm buildingUm);
    void Delete(Guid guid);
    PaginatedResult<BuildingVm> GetAll(Guid? companyGuid, int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder);
}