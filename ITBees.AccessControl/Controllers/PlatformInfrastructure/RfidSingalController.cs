using ITBees.AccessControl.Interfaces;
using ITBees.AccessControl.Interfaces.ViewModels;
using ITBees.RestfulApiControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Controllers.PlatformInfrastructure;

public class RfidSignalController :RestfulControllerBase<RfidSignalController>
{
    private readonly IAccessControlSignalReceived _accessControlSignalReceived;

    public RfidSignalController(ILogger<RfidSignalController> logger, IAccessControlSignalReceived accessControlSignalReceived) : base(logger)
    {
        _accessControlSignalReceived = accessControlSignalReceived;
    }
    
    [HttpGet]
    public IActionResult Get(string mac, string ip, string name, string id, string inout, string ts, string io, string put)
    {
        ReceivedRfidSignalIm receivedRfidSignalIm = new ReceivedRfidSignalIm(mac, ip, name, id, inout, ts, io, put);

        return ReturnOkResult(()=>_accessControlSignalReceived.Handle(receivedRfidSignalIm, base.GetClientIp()));
    }
}