using ITBees.AccessControl.Controllers.PlatformOperator;
using ITBees.AccessControl.Controllers.PlatformOperator.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;
using ITBees.Models.Users;
using ITBees.UserManager.Interfaces;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Services.PlatformOperator;

public class DeactivateAccessCardsService : IDeactivateAccessCardsService
{
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IWriteOnlyRepository<AccessCard> _accessCardRwRepo;
    private readonly ILogger<DeactivateAccessCardsService> _logger;

    public DeactivateAccessCardsService(IAspCurrentUserService aspCurrentUserService, 
        IWriteOnlyRepository<AccessCard> accessCardRwRepo,
        ILogger<DeactivateAccessCardsService> logger)
    {
        _aspCurrentUserService = aspCurrentUserService;
        _accessCardRwRepo = accessCardRwRepo;
        _logger = logger;
    }

    public DeactivateAccessCardResultVm Deactivate(AccessCardsToDeactivateIm accessCardsToDeactivate)
    {
        var cu = _aspCurrentUserService.GetCurrentUserGuid().Value;
        var deactivateCardError = new List<Guid>();

        if (_aspCurrentUserService.TryCanIDoForCompany(TypeOfOperation.Rw,
                _aspCurrentUserService.GetCurrentUser().LastUsedCompanyGuid))
        {
            foreach (var guid in accessCardsToDeactivate.Guids)
            {
                try
                {
                    var result = _accessCardRwRepo.UpdateData(x => x.Guid == guid, x =>
                    {
                        x.IsActive = false;
                        x.DeactivatedByGuid = cu;
                        x.Deactivated = DateTime.Now;
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error while deactivate card ({guid}): {e.Message} {e.InnerException?.Message}" );
                    deactivateCardError.Add(guid);
                }
            }
        }

        return new DeactivateAccessCardResultVm()
        {
            CardsNotDeactivated = deactivateCardError
        };
    }
}