using ITBees.AccessControl.Interfaces.Models;

namespace ITBees.AccessControl.Interfaces.ViewModels;

public class UnauthorizedRfidDeviceVm
{
    public UnauthorizedRfidDeviceVm()
    {
        
    }
    public UnauthorizedRfidDeviceVm(UnauthorizedRfidDevice x)
    {
        Ip = x.Ip;
        DeviceName = x.DeviceName;
        FirstSeenDate = x.FirstSeenDate;
        this.Guid = x.Guid;
        IpForwarded  = x.IpForwarded;
        LastConnection = x.LastConnection;
        Mac = x.Mac;
    }

    public string Ip { get; set; }

    public string DeviceName { get; set; }

    public DateTime FirstSeenDate { get; set; }

    public Guid Guid { get; set; }

    public string IpForwarded { get; set; }

    public DateTime? LastConnection { get; set; }

    public string Mac { get; set; }
}