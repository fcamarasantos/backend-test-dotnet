using System.Dynamic;
using System.Net;

namespace ParkingLotManager.ReportApi.DTOs;

public class ParkingLotGenericResponseDto<T> where T : class
{
    public ParkingLotGenericResponseDto()
    {        
    }

    public ParkingLotGenericResponseDto(T data)
    {
        Data = data;
    }

    public ParkingLotGenericResponseDto(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }
    
    public ParkingLotGenericResponseDto(string error)
    {
        Errors.Add(error);
    }

    public ParkingLotGenericResponseDto(List<string> errors)
    {
        Errors = errors;
    }

    public HttpStatusCode StatusCode { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; } = new();
}
