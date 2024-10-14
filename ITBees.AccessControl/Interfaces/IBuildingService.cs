using ITBees.AccessControl.Controllers.PlatformAdmin;
using ITBees.AccessControl.Controllers.PlatformAdmin.Models;

namespace ITBees.AccessControl.Interfaces;

public interface IBuildingService
{
    BuildingVm Get(Guid guid);
    BuildingVm Create(BuildingIm buildingIm);
    BuildingVm Update(BuildingUm buildingUm);
    void Delete(Guid guid);
}