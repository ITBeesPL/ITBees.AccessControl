using ITBees.Models.Hardware;

namespace ITBees.AccessControl.Controllers.PlatformAdmin.Models;

public class DeviceHubVm
{
    public DeviceHubVm()
    {
        
    }
    public DeviceHubVm(DeviceHub x)
    {
        Guid = x.Guid;
        Name = x.Name;
        Description = x.Description;
        CreatedBy = x.CreatedBy?.DisplayName;
        CreatedByGuid = x.CreatedByGuid;
        Created = x.Created;
        IsActive = x.IsActive;
    }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public Guid CreatedByGuid { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
}