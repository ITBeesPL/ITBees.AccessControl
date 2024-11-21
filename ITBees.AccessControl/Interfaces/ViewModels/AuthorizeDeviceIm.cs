﻿namespace ITBees.AccessControl.Interfaces.ViewModels;

public class AuthorizeDeviceIm
{
    public string Ip { get; set; }
    public string Mac { get; set; }
    public Guid? CompanyGuid { get; set; }
    public string DeviceName { get; set; }
    public Guid DeviceGuid { get; set; }
    public Guid? BuildingGuid { get; set; }
    public int IpNetworkAddressId { get; set; }
    public string TriggerApiEndpoint { get; set; }
    public Guid DeviceTypeGuid { get; set; }
}