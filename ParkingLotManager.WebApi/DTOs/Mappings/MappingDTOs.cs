using AutoMapper;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.DTOs.Mappings;

public class MappingDTOs : Profile
{
    public MappingDTOs()
    {
        CreateMap<Vehicle, VehicleDTO>().ReverseMap();
        CreateMap<Company, CompanyDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
