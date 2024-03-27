using Newtonsoft.Json;
using ParkingLotManager.ReportApi.DTOs;
using ParkingLotManager.ReportApi.Interfaces;
using ParkingLotManager.ReportApi.Models;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ParkingLotManager.ReportApi.REST;

public class ParkingLotManagerWebApiService : IVehicleQuery
{
    private readonly HttpClient _httpClient;
    private const string Uri = Configuration.Uri;
    private const string ApiKeyName = Configuration.ApiKeyName;
    private const string ApiKey = Configuration.ApiKey;


    public ParkingLotManagerWebApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<VehicleModel>> GetVehiclesAsync()
    {
        var uri = new StringBuilder();
        uri.Append(Uri + $"/?{ApiKeyName}={ApiKey}");

        var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
        var response = new List<VehicleModel>();
        
        var responseParkingLotWebApi = await _httpClient.SendAsync(request);
        var contentResponse = await responseParkingLotWebApi.Content.ReadAsStringAsync();
        var objectResponse = JsonConvert.DeserializeObject<List<VehicleModel>>(contentResponse);

        if (!responseParkingLotWebApi.IsSuccessStatusCode)
            throw new Exception(responseParkingLotWebApi.StatusCode.ToString());
        else
            response = objectResponse;
        
        return response;
    }
}
