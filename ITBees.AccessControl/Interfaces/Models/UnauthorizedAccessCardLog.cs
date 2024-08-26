namespace ITBees.AccessControl.Interfaces.Models;

public class UnauthorizedAccessCardLog
{
    public int Id { get; set; }
    public string CardId { get; set; }
    public DateTime EventDate { get; set; }
    public string Message { get; set; }
    public RfidReaderDevice LastConnectedTroughRfidReaderDevice { get; set; }
    public Guid LastConnectedTroughRfidReaderDeviceGuid { get; set; }
}