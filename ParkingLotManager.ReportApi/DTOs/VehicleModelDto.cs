namespace ParkingLotManager.ReportApi.DTOs;

public class VehicleModelDto
{
    public string licensePlate { get; set; }
    public string brand { get; set; }
    public string model { get; set; }
    public string color { get; set; }
    public int type { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime lastUpdateDate { get; set; }
    public bool isActive { get; set; }
    public object company { get; set; }
    public string companyName { get; set; }
}
