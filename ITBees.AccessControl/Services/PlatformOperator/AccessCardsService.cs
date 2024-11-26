using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;
using ITBees.Models.Users;
using ITBees.RestfulApiControllers.Exceptions;
using ITBees.RestfulApiControllers.Models;
using ITBees.UserManager.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ITBees.AccessControl.Services.PlatformOperator;

class AccessCardsService : IAccessCardsService
{
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IReadOnlyRepository<AccessCard> _accessCardsRoRepo;

    public AccessCardsService(IAspCurrentUserService aspCurrentUserService,
        IReadOnlyRepository<AccessCard> accessCardsRoRepo)
    {
        _aspCurrentUserService = aspCurrentUserService;
        _accessCardsRoRepo = accessCardsRoRepo;
    }

    public PaginatedResult<AccessCardVm> GetMyCompanyCards(int? page, int? pageSize, string? sortColumn, SortOrder? sortOrder)
    {
        var cu = _aspCurrentUserService.GetCurrentUser();
        if (_aspCurrentUserService.TryCanIDoForCompany(TypeOfOperation.Ro, cu.LastUsedCompanyGuid) == false)
        {
            var message = "You don't have enought rights to get this data.";
            throw new FasApiErrorException(new FasApiErrorVm(message, StatusCodes.Status403Forbidden, ""));
        }

        PaginatedResult<AccessCard> results = _accessCardsRoRepo.GetDataPaginated(x => x.IsActive && x.CompanyGuid == cu.LastUsedCompanyGuid, new SortOptions(page, pageSize, sortColumn, sortOrder),
            x => x.AccessCardType, x => x.CreatedBy);

        var mappedResults = results.MapTo(ac => new AccessCardVm
        {
            CardId = ac.CardId,
            AccessCardType = ac.AccessCardType.Name,
            InvitationSend = ac.InvitationSend,
            Guid = ac.Guid,
            LastUsedDate = ac.LastUsedDateTime,
            OwnerEmail = ac.OwnerEmail,
            OwnerName = ac.OwnerName,
            ValidTo = ac.ValidDate,
            AccessCardGroups = ac.AccessCardCardGroups.Select(x=>new AccessCardGroupVm(x.AccessCardGroup)).ToList()
        });
        return mappedResults;
    }
}