using ITBees.AccessControl.Interfaces.ViewModels;

namespace ITBees.AccessControl.Interfaces;

public interface IAuthorizeAccessCardsService
{
    AuthorizeCardsResultVm Authorize(AuthorizeAccessCardsIm authorizeAccessCardsIm);
}