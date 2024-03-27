using ParkingLotManager.WebApi.Models;
using AutoMapper;
using ParkingLotManager.WebApi.DTOs;

namespace ParkingLotManager.WebApi.Extensions;

public static class EntitiesExtensions
{
    public static List<dynamic> VehiclesToDtoList(this List<Vehicle> vehicleList, List<Vehicle> vehicles, IMapper mapper)
    {
        var resultList = new List<dynamic>();
        foreach(var item in vehicles)
        {
            if (!item.IsActive)
                continue;
            var vehicleMapping = mapper.Map<VehicleDTO>(item);
            var vehicleDto = vehicleMapping.Display();
            resultList.Add(vehicleDto);
        }
        return resultList;
    }
    
    public static List<dynamic> CompaniesToDtoList(this List<Company> companyList, List<Company> companies, IMapper mapper)
    {
        var resultList = new List<dynamic>();
        foreach(var item in companies)
        {
            var companyMapping = mapper.Map<VehicleDTO>(item);
            var companyDto = companyMapping.Display();
            resultList.Add(companyDto);
        }
        return resultList;
    }
}
