using ITBees.AccessControl.Controllers.PlatformOperator.Models;

namespace ITBees.AccessControl.Interfaces;

public interface IDeactivateAccessCardsService
{
    DeactivateAccessCardResultVm Deactivate(AccessCardsToDeactivateIm accessCardsToDeactivate);
}