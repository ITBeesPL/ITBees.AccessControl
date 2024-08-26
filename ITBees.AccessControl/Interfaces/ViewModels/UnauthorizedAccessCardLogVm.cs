using ITBees.AccessControl.Interfaces.Models;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class UnauthorizedAccessCardLogVm
{
    public UnauthorizedAccessCardLogVm()
    {
        
    }
    public UnauthorizedAccessCardLogVm(UnauthorizedAccessCardLog x)
    {
        CardId = x.CardId;
        EventDate = x.EventDate;
        Id = x.Id;
        DeviceName = x.LastConnectedTroughRfidReaderDevice.DeviceName;
        DeviceIp = x.LastConnectedTroughRfidReaderDevice.Ip;
    }

    public DateTime EventDate { get; set; }

    public int Id { get; set; }

    public string DeviceIp { get; set; }

    public string DeviceName { get; set; }

    public string CardId { get; set; }
}