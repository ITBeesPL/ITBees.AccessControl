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
                    Created = DateTime.Now
                }));
            }
            

            return new AllowedAccessCardsVm(result);
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Duplicate"))
            {
                var cardAlreadyAllowedForAuthorization = "Card already allowed for authorization";
                throw new FasApiErrorException(new FasApiErrorVm(cardAlreadyAllowedForAuthorization, 400, ""));
            }

            var error = "Error " + e.Message;
            throw new FasApiErrorException(new FasApiErrorVm(error, 500, ""));
        }
    }

    public bool IsCardAllowedToAuthorize(string cardId)
    {
        var card = _allowedAccessCardRoRepo.GetData(x => x.CardId == cardId).FirstOrDefault();
        if (card == null)
        {
            return false;
        }

        return true;
    }

    public PaginatedResult<AllowedAccessCardVm> GetCards(int page, int pageSize, string sortColumn, SortOrder sortOrder)
    {
        PaginatedResult<AllowedAccessCard> results = _allowedAccessCardRoRepo.GetDataPaginated(x =>true,  page, pageSize, sortColumn, sortOrder, x => x.CreatedBy);

        var mappedResults = results.MapTo(ac => new AllowedAccessCardVm()
        {
            CardId = ac.CardId,
            Created = ac.Created,
            CreatedBy = ac.CreatedBy.LastName,
            CreatedByGuid = ac.CreatedByGuid,
            Guid = ac.Guid
        });

        return mappedResults;
    }
}