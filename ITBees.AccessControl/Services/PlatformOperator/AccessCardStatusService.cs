using ITBees.AccessControl.Controllers.PlatformOperator.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Services.PlatformOperator;

public class AccessCardStatusService : IAccessCardStatusService
{
    private readonly IReadOnlyRepository<AccessCard> _accessCardRoRepo;
    private readonly IReadOnlyRepository<AllowedAccessCard> _allowedAccessCardRoRepo;

    public AccessCardStatusService(
        IReadOnlyRepository<AccessCard> accessCardRoRepo,
        IReadOnlyRepository<AllowedAccessCard> allowedAccessCardRoRepo
    )
    {
        _accessCardRoRepo = accessCardRoRepo;
        _allowedAccessCardRoRepo = allowedAccessCardRoRepo;
    }
    public AccessCardStatusVm Check(string cardId)
    {
        cardId = RfidCardHexConverter.GetHexFormat(cardId);
        
        if (_allowedAccessCardRoRepo.GetData(x => x.CardId == cardId).FirstOrDefault() == null)
        {
            return new AccessCardStatusVm()
            {
                IsAllowedToActivate = false, IsAlreadyActivated = false,
                Message = "Card not allowed to access in octopark system, pleas contact with Octopark Team"
            };
        }

        if (_accessCardRoRepo.GetData(x => x.CardId == cardId).FirstOrDefault() == null)
        {
            return new AccessCardStatusVm()
            {
                IsAllowedToActivate = true,
                IsAlreadyActivated = false,
                Message = "Card can be activated"
            };
        }
        else
        {
            return new AccessCardStatusVm()
            {
                IsAllowedToActivate = true,
                IsAlreadyActivated = true,
                Message = "Card already activated"
            };
        }
    }

    public IsCardAllowedResult IsCardAllowedToAuthorize(string cardId)
    {
        cardId = RfidCardHexConverter.GetHexFormat(cardId);
        var card = _allowedAccessCardRoRepo.GetData(x => x.CardId == cardId).FirstOrDefault();
        if (card == null)
        {
            return new IsCardAllowedResult() { Allowed = false, CardGuid = null , AccessCardTypeId = card.AccessCardTypeId};
        }

        return new IsCardAllowedResult() { Allowed = true, CardGuid = card.Guid , AccessCardTypeId = card.AccessCardTypeId };
    }
}