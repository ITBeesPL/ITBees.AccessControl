namespace ITBees.AccessControl.Controllers.PlatformAdmin;

public class DeviceHubVm
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
}