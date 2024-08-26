namespace ITBees.AccessControl.Interfaces.Models;

public class UnauthorizedRfidDevice
{
    public Guid Guid { get; set; }
    public string Ip { get; set; }
    public string IpForwarded { get; set; }
    public string Mac { get; set; }
    public DateTime FirstSeenDate { get; set; }
    public DateTime? LastConnection { get; set; }
    public string DeviceName { get; set; }
}