using ITBees.AccessControl.Controllers.PlatformOperator.Models;

namespace ITBees.AccessControl.Interfaces;

public interface IAccessCardStatusService
{
    AccessCardStatusVm Check(string cardId);
}