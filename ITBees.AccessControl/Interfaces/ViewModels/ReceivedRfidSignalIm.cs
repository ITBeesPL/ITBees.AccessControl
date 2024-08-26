namespace ITBees.AccessControl.Interfaces.ViewModels;

public class ReceivedRfidSignalIm
{
    public string Put { get; }
    public string Io { get; }
    public string Ts { get; }
    public string Inout { get; }
    public string Id { get; }
    public string Name { get; }
    public string Ip { get; }
    public string Mac { get; }

    public ReceivedRfidSignalIm(string mac, string ip, string name, 
        string id, string inout, string ts, string io, string put)
    {
        Mac = mac;
        Ip = ip;
        Name = name;
        Id = id;
        Inout = inout;
        Ts = ts;
        Io = io;
        Put = put;
    }
}