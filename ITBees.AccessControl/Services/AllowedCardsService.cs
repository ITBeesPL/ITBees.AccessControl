using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.Models;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.RestfulApiControllers.Exceptions;
using ITBees.RestfulApiControllers.Models;
using ITBees.UserManager.Interfaces;

namespace ITBees.AccessControl.Services;

public class AllowedCardsService : IAllowedCardsService
{
    private readonly IReadOnlyRepository<AllowedAccessCard> _allowedAccessCardRoRepo;
    private readonly IWriteOnlyRepository<AllowedAccessCard> _allowedAccessCardRwRepo;
    private readonly IAspCurrentUserService _aspCurrentUserService;

    public AllowedCardsService(IReadOnlyRepository<AllowedAccessCard> allowedAccessCardRoRepo,
        IWriteOnlyRepository<AllowedAccessCard> allowedAccessCardRwRepo,
        IAspCurrentUserService aspCurrentUserService
        )
    {
        _allowedAccessCardRoRepo = allowedAccessCardRoRepo;
        _allowedAccessCardRwRepo = allowedAccessCardRwRepo;
        _aspCurrentUserService = aspCurrentUserService;
    }

    public AllowedAccessCardsVm RegisterCard(AllowedAccessCardsIm allowedAccessCards)
    {
        try
        {
            var result = new List<AllowedAccessCard>();
            foreach (var allowedAccessCardIm in allowedAccessCards.AllowedAccessCards)
            {
                result.Add(_allowedAccessCardRwRepo.InsertData(new AllowedAccessCard()
                {
                    CardId = allowedAccessCardIm.CardId,
                    CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
                    Created = DateTime.Now,
                    AccessCardTypeId = allowedAccessCardIm.AccessCardTypeId,
                    CompanyGuid = allowedAccessCardIm.CompanyGuid
                }));
            }


            return new AllowedAccessCardsVm(result);
        }
        catch (Exception e)
        {
            if (e.InnerException.Message.Contains("Duplicate"))
            {
                var cardAlreadyAllowedForAuthorization = "Card already allowed for authorization";
                throw new FasApiErrorException(new FasApiErrorVm(cardAlreadyAllowedForAuthorization, 400, ""));
            }

            var error = "Error " + e.Message;
            throw new FasApiErrorException(new FasApiErrorVm(error, 500, ""));
        }
    }

    public IsCardAllowedResult IsCardAllowedToAuthorize(string cardId)
    {
        var card = _allowedAccessCardRoRepo.GetData(x => x.CardId == cardId).FirstOrDefault();
        if (card == null)
        {
            return new IsCardAllowedResult() { Allowed = false, CardGuid = null , AccessCardTypeId = card.AccessCardTypeId};
        }

        return new IsCardAllowedResult() { Allowed = true, CardGuid = card.Guid };
    }

    public PaginatedResult<AllowedAccessCardVm> GetCards(int? page, int? pageSize, AllowedAccessCardSortOptions? sortColumn, SortOrder? sortOrder)
    {
        PaginatedResult<AllowedAccessCard> results = _allowedAccessCardRoRepo.GetDataPaginated(x => true, new SortOptions(page, pageSize,sortColumn, sortOrder), x => x.CreatedBy, x => x.Company);

        var mappedResults = results.MapTo(ac => new AllowedAccessCardVm()
        {
            CardId = ac.CardId,
            Created = ac.Created,
            CreatedBy = ac.CreatedBy.LastName,
            CreatedByGuid = ac.CreatedByGuid,
            Guid = ac.Guid,
            AccessCardTypeId = ac.AccessCardTypeId,
            ActivationDate = ac.ActivationDate,
            CompanyGuid = ac.CompanyGuid,
            CompanyName = ac.Company?.CompanyName,
            IsActive = ac.IsActive
        });

        return mappedResults;
    }

    public DeleteAccessCardResultVm Delete(AllowedAccessCardsDm allowedAccessCardsDm)
    {
        var failedGuids = new List<Guid>();
        var successGuids = new List<Guid>();
        foreach (var card in allowedAccessCardsDm.Guids)
        {
            try
            {
                var result = _allowedAccessCardRwRepo.DeleteData(x => x.Guid == card);
                if (result == 0)
                {
                    failedGuids.Add(card);
                }
                else
                {
                    successGuids.Add(card);
                }

            }
            catch (Exception e)
            {
                failedGuids.Add(card);
            }
        }

        return new DeleteAccessCardResultVm() { DeletedCardsFail = failedGuids, DeletedCardsSuccess = successGuids };
    }

    public void SetCardAsActive(Guid? cardGuid)
    {
        _allowedAccessCardRwRepo.UpdateData(x => x.Guid == cardGuid, x =>
        {
            x.IsActive = true;
            x.ActivationDate = DateTime.Now;
        });
    }

    public AllowedAccessCardVm UpdateCard(AllowedAccessCardUm allowedAccessCardUm)
    {
        var result = _allowedAccessCardRwRepo.UpdateData(x => x.Guid == allowedAccessCardUm.Guid, x =>
        {
            x.AccessCardTypeId = allowedAccessCardUm.AccessCardTypeId;
            x.CompanyGuid = allowedAccessCardUm.CompanyGuid;
        }).FirstOrDefault();
        if (result != null)
            return new AllowedAccessCardVm(result);

        throw new ResultNotFoundException($"Card with guid {allowedAccessCardUm.Guid} not found ");
    }
}