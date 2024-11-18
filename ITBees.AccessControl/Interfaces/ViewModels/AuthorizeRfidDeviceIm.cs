namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AuthorizeRfidDeviceIm
{
    public string Ip { get; set; }
    public string Mac { get; set; }
    public Guid? CompanyGuid { get; set; }
    public string DeviceName { get; set; }
    public Guid DeviceGuid { get; set; }
    public Guid? BuildingGuid { get; set; }
    public int IpNetworkAddressId { get; set; }
    public string IpAddress { get; set; }
}