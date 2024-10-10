using ITBees.AccessControl.Controllers.PlatformInfrastructure.Models;

namespace ITBees.AccessControl.Services.Common;

public interface IAccessCardTypesService
{
    List<AccessCardTypeVm> Get();
}