using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.AccessControl.Services.PlatformOperator;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;
using ITBees.RestfulApiControllers.Exceptions;
using ITBees.RestfulApiControllers.Models;
using ITBees.UserManager.Interfaces;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Services;

public class AuthorizeAccessCardsService : IAuthorizeAccessCardsService
{
    private readonly IAllowedCardsService _allowedCardsService;
    private readonly IWriteOnlyRepository<AccessCard> _accessCardRwRepo;
    private readonly IWriteOnlyRepository<AccessCardCardGroup> _accessCardCardGroupRwRepo;
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly ILogger<AuthorizeAccessCardsService> _logger;

    public AuthorizeAccessCardsService(IAllowedCardsService allowedCardsService,
        IWriteOnlyRepository<AccessCard> accessCardRwRepo,
        IWriteOnlyRepository<AccessCardCardGroup> accessCardCardGroupRwRepo,
        IAspCurrentUserService aspCurrentUserService,
        ILogger<AuthorizeAccessCardsService> logger)
    {
        _allowedCardsService = allowedCardsService;
        _accessCardRwRepo = accessCardRwRepo;
        _accessCardCardGroupRwRepo = accessCardCardGroupRwRepo;
        _aspCurrentUserService = aspCurrentUserService;
        _logger = logger;
    }
    public AuthorizeCardsResultVm Authorize(AuthorizeAccessCardsIm authorizeAccessCardsIm)
    {
        var result = new AuthorizeCardsResultVm();

        foreach (var accessCard in authorizeAccessCardsIm.AccessCards)
        {
            //todo security check!!!!

            var isCardAllowedToAuthorize = _allowedCardsService.IsCardAllowedToAuthorize(accessCard.CardId);
            if (isCardAllowedToAuthorize.Allowed)
            {
                try
                {
                    var resultAccessCard = _accessCardRwRepo.InsertData(new AccessCard()
                    {
                        CardId = RfidCardHexConverter.GetHexFormat(accessCard.CardId),
                        Created = DateTime.Now,
                        IsActive = accessCard.IsActive,
                        CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
                        InvitationSend = accessCard.InvitationSend,
                        OwnerEmail = accessCard.OwnerEmail,
                        OwnerName = accessCard.OwnerName,
                        ValidDate = accessCard.ValidDate,
                        AccessCardTypeId = isCardAllowedToAuthorize.AccessCardTypeId,
                        CompanyGuid = accessCard.CompanyGuid.Value
                    });

                    result.AllowedAccessCards.Add(new AllowedAccessCardVm(resultAccessCard));

                    try
                    {
                        if (accessCard.AcGroupGuids != null || accessCard.AcGroupGuids.Any())
                        {
                            var accessCardCardGroups = accessCard.AcGroupGuids.Select(x => new AccessCardCardGroup()
                            {
                                AccessCardGroupGuid = x,
                                AccessCardGuid = resultAccessCard.Guid
                            }).ToList();

                            _accessCardCardGroupRwRepo.InsertData(accessCardCardGroups);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Error while inserting groups to database, message {e.Message} {e.InnerException}");
                    }

                    _allowedCardsService.SetCardAsActive(isCardAllowedToAuthorize.CardGuid);
                }
                catch (Exception e)
                {
                    if (e.InnerException.Message.Contains("Duplicate"))
                    {
                        throw new FasApiErrorException(new FasApiErrorVm("Card already registerd", 400, ""));
                    }
                    throw new FasApiErrorException(new FasApiErrorVm($"Error on saveing card {accessCard.CardId} to database, message :{e.InnerException.Message}", 400, ""));
                }
            }
            else
            {
                result.UnauthorizedCards.Add(accessCard.CardId);
            }
        }

        return result;
    }
}