﻿using ITBees.AccessControl.Controllers.PlatformAdmin;
using ITBees.AccessControl.Controllers.PlatformAdmin.Models;
using ITBees.AccessControl.Interfaces;
using ITBees.Interfaces.Repository;
using ITBees.Models.Buildings;
using ITBees.Models.Common;
using ITBees.UserManager.Interfaces;
using Microsoft.Extensions.Logging;

namespace ITBees.AccessControl.Services.PlatformAdmin;

public class BuildingService : IBuildingService
{
    private readonly ILogger<BuildingService> _logger;
    private readonly IAspCurrentUserService _aspCurrentUserService;
    private readonly IReadOnlyRepository<Building> _buildingRoRepo;
    private readonly IWriteOnlyRepository<Building> _buildingRwRepo;

    public BuildingService(ILogger<BuildingService> logger,
        IAspCurrentUserService aspCurrentUserService,
        IReadOnlyRepository<Building> buildingRoRepo,
        IWriteOnlyRepository<Building> buildingRwRepo)
    {
        _logger = logger;
        _aspCurrentUserService = aspCurrentUserService;
        _buildingRoRepo = buildingRoRepo;
        _buildingRwRepo = buildingRwRepo;
    }

    public BuildingVm Get(Guid guid)
    {
        var result = _buildingRoRepo.GetData(x => x.Guid == guid).FirstOrDefault();
        return new BuildingVm(result);
    }

    public BuildingVm Create(BuildingIm x)
    {
        var building = _buildingRwRepo.InsertData(new Building()
        {
            Created = DateTime.Now,
            CreatedByGuid = _aspCurrentUserService.GetCurrentUserGuid().Value,
            GpsLocation = x.GpsLocation == null ? null : new GpsLocation()
            {
                Latitude = x.GpsLocation.Latitude,
                Longitude = x.GpsLocation.Longitude
            },
            IsActive = x.IsActive,
            Name = x.Name,
            CompanyGuid = x.CompanyGuid,
            BuildingDeviceHubs = new List<BuildingDeviceHub>()
        });

        var createdBuilding = _buildingRoRepo.GetFirst(x => x.Guid == building.Guid, x => x.CreatedBy, x => x.GpsLocation, x => x.BuildingDeviceHubs);

        return new BuildingVm(createdBuilding);
    }

    public BuildingVm Update(BuildingUm buildingUm)
    {
        _buildingRwRepo.UpdateData(x => x.Guid == buildingUm.Guid, x =>
        {
            x.GpsLocation = new GpsLocation() { Latitude = buildingUm.GpsLocation.Latitude, Longitude = buildingUm.GpsLocation.Longitude };
            x.IsActive = buildingUm.IsActive;
            x.Name = buildingUm.Name;
            x.CompanyGuid = buildingUm.CompanyGuid;
        });
        var result = _buildingRoRepo.GetData(x => x.Guid == buildingUm.Guid, x => x.BuildingDeviceHubs,
            x => x.CreatedBy, x => x.GpsLocation).First();

        return new BuildingVm(result);
    }

    public void Delete(Guid guid)
    {
        _buildingRwRepo.DeleteData(x => x.Guid == guid);
    }

    public PaginatedResult<BuildingVm> GetAll(Guid companyGuid, int page, int pageSize, string sortColumn, SortOrder sortOrder)
    {
        return _buildingRoRepo.GetDataPaginated(x => true, page, pageSize, sortColumn, sortOrder).MapTo(x => new BuildingVm(x));
    }
}