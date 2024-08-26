using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services;

class AuthorizeAccessCardsService : IAuthorizeAccessCardsService
{
    private readonly IAllowedCardsService _allowedCardsService;
    private readonly IWriteOnlyRepository<AccessCard> _accessCardRwRepo;
    private readonly IAspCurrentUserService _aspCurrentUserService;

    public AuthorizeAccessCardsService(IAllowedCardsService allowedCardsService,
        IWriteOnlyRepository<AccessCard> accessCardRwRepo,
        IAspCurrentUserService aspCurrentUserService)
    {
        _allowedCardsService = allowedCardsService;
        _accessCardRwRepo = accessCardRwRepo;
        _aspCurrentUserService = aspCurrentUserService;
    }
    public AuthorizeCardsResultVm Authorize(AuthorizeAccessCardsIm authorizeAccessCardsIm)
    {
        var result = new AuthorizeCardsResultVm();

        foreach (var accessCard in authorizeAccessCardsIm.AccessCards)
        {
            //todo security check!!!!

            if (_allowedCardsService.IsCardAllowedToAuthorize(accessCard.CardId))
            {
                var resultAccessCard = _accessCardRwRepo.InsertData(new AccessCard()
                {
                    CardId = accessCard.CardId,
                    Created = DateTime.Now,
                    IsActive = accessCard.IsActive,
                    CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
                    //AccessCardType = new AccessCardType(){ Id = accessCard.AccessCardType.Id },
                    InvitationSend = accessCard.InvitationSend,
                    OwnerEmail = accessCard.OwnerEmail,
                    OwnerName = accessCard.OwnerName,
                    ValidDate = accessCard.ValidDate
                });
                result.AllowedAccessCards.Add(new AllowedAccessCardVm(resultAccessCard));
            }
            else
            {
                result.UnauthorizedCards.Add(accessCard.CardId);
            }
        }

        return result;
    }
}