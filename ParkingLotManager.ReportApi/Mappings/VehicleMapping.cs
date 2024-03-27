using AutoMapper;
using ParkingLotManager.ReportApi.DTOs;
using ParkingLotManager.ReportApi.Models;

namespace ParkingLotManager.ReportApi.Mappings;

public class VehicleMapping : Profile
{
    public VehicleMapping()
    {
        CreateMap<VehicleModel, VehicleModelDto>();

        CreateMap(typeof(ParkingLotGenericResponseDto<>), typeof(ParkingLotGenericResponseDto<>));        
    }
}
