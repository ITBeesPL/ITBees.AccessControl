using ITBees.AccessControl.Controllers.PlatformOperator.Models;
using ITBees.AccessControl.Services;

namespace ITBees.AccessControl.Interfaces;

public interface IAccessCardStatusService
{
    AccessCardStatusVm Check(string cardId);
    IsCardAllowedResult IsCardAllowedToAuthorize(string cardId);
}