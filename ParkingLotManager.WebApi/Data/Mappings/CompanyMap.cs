using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data.Mappings;

public class CompanyMap : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Table
        builder.ToTable("Company");

        // Primary Key
        builder.HasKey(x => x.Name);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.OwnsOne(x => x.Cnpj, cnpj =>
        {
            cnpj.Property(p => p.CnpjNumber)
                .IsRequired()
                .HasColumnName("CnpjNumber")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(14);
        });

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(p => p.Street)
                .IsRequired()
                .HasColumnName("Street")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            address.Property(p => p.City)
                .IsRequired()
                .HasColumnName("City")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            address.Property(p => p.ZipCode)
                .IsRequired()
                .HasColumnName("ZipCode")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30);
        });

        builder.Property(x => x.Telephone)
            .IsRequired()
            .HasColumnName("Telephone")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);

        builder.Property(x => x.CarSlots)
            .IsRequired()
            .HasColumnName("CarSlots")
            .HasColumnType("INT");

        builder.Property(x => x.MotorcycleSlots)
            .IsRequired()
            .HasColumnName("MotorcycleSlots")
            .HasColumnType("INT");
    }
}