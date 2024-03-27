using ParkingLotManager.WebApi.Models;
using System.Collections.Generic;

namespace ParkingLotManager.WebApi.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public IList<User> Users { get; set; }
}