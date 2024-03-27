using Microsoft.EntityFrameworkCore;
using ParkingLotManager.WebApi.Data.Mappings;
using ParkingLotManager.WebApi.Enums;
using ParkingLotManager.WebApi.Models;
using System.Diagnostics;

namespace ParkingLotManager.WebApi.Data;

public class AppDataContext : DbContext
{
    protected AppDataContext()
    {
    }

    public AppDataContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var pendingChanges = ChangeTracker.Entries<Vehicle>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in pendingChanges)
        {
            var vehicle = entry.Entity;
            var company = Companies.FirstOrDefault(c => c.Name == vehicle.CompanyName);

            if (company != null)
            {
                if (vehicle.Type == EVehicleType.Car)
                {
                    company.CarSlots--;
                }
                else if (vehicle.Type == EVehicleType.Motorcycle)
                {
                    company.MotorcycleSlots--;
                }
            }
        }

        // Salve as alterações no banco de dados
        return await base.SaveChangesAsync(cancellationToken);
    }
}