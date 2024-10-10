using ITBees.AccessControl.Controllers.PlatformInfrastructure.Models;
using ITBees.Interfaces.Repository;
using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Services.Common;

public class AccessCardTypesService : IAccessCardTypesService
{
    private readonly IReadOnlyRepository<AccessCardType> _readOnlyRepository;

    public AccessCardTypesService(IReadOnlyRepository<AccessCardType> readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
    }
    public List<AccessCardTypeVm> Get()
    {
        return _readOnlyRepository.GetData(x => true).Select(x=>new AccessCardTypeVm(x)).ToList();
    }
}