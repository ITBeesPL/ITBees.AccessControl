using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers;

[Authorize]
public class AuthorizeAccessCardsController : RestfulControllerBase<AuthorizeAccessCardsController>
{
    private readonly IAuthorizeAccessCardsService _authorizeAccessCardsService;

    public AuthorizeAccessCardsController(ILogger<AuthorizeAccessCardsController> logger,
        IAuthorizeAccessCardsService authorizeAccessCardsService) : base(logger)
    {
        _authorizeAccessCardsService = authorizeAccessCardsService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] AuthorizeAccessCardsIm authorizeAccessCardsIm)
    {
        return ReturnOkResult(() => _authorizeAccessCardsService.Authorize(authorizeAccessCardsIm));
    }
}