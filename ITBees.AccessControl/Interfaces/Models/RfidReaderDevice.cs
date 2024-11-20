using ITBees.Models.Buildings;
using ITBees.Models.Companies;
using ITBees.Models.Hardware.Infrastructure;

namespace ITBees.AccessControl.Interfaces.Models;

public class RfidReaderDevice
{
    public Guid Guid { get; set; }
    public string Ip { get; set; }
    public string Mac { get; set; }
    public Company Company { get; set; }
    public Guid? CompanyGuid { get; set; }
    public DateTime? LastConnection { get; set; }
    public string DeviceName { get; set; }
    public Building Building { get; set; }
    public Guid? BuildingGuid { get; set; }
    public IpAddress IpAddress { get; set; }
    public int? IpAddressId { get; set; }
}