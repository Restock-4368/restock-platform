
using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Provides extension methods to configure the domain model using Entity Framework Core.
/// </summary> 
public static class ModelBuilderExtensions
{ 
    public static void ApplyResourceConfiguration(this ModelBuilder builder)
    {
          // ========== Resource mappings ==========

        // OrderToSupplier context
        builder.Entity<OrderToSupplier>(o =>
        {
            o.HasKey(x => x.Id);
            o.Property(x => x.Id)
             .HasConversion(id => id, value => value)
             .ValueGeneratedOnAdd();

            o.Property(x => x.CreatedDate)
             .HasColumnName("date")
             .IsRequired();

            o.Property(x => x.EstimatedShipDate)
             .HasColumnName("estimated_ship_date")
             .IsRequired(false);

            o.Property(x => x.EstimatedShipTime)
             .HasColumnName("estimated_ship_time")
             .IsRequired(false);

            o.Property(x => x.Description)
             .HasColumnName("description")
             .HasMaxLength(500)
             .IsRequired(false);

            o.Property(x => x.AdminRestaurantId)
             .HasColumnName("admin_restaurant_id")
             .IsRequired();

            o.Property(x => x.SupplierId)
             .HasColumnName("supplier_id")
             .IsRequired();

            o.Property(x => x.State)
             .HasColumnName("state")
             .HasConversion<string>()
             .IsRequired();

            o.Property(x => x.Situation)
             .HasColumnName("situation")
             .HasConversion<string>()
             .IsRequired();

            o.Property(x => x.RequestedProductsCount)
             .HasColumnName("requested_products_count")
             .IsRequired();

            o.Property(x => x.TotalPrice)
             .HasColumnName("total_price")
             .HasConversion(p => p, v => v)
             .IsRequired();

            o.Property(x => x.PartiallyAccepted)
             .HasColumnName("partially_accepted")
             .IsRequired();
 
            // navigation to batches
            o.Navigation(x => x.RequestedBatches)
             .UsePropertyAccessMode(PropertyAccessMode.Field);

            o.HasMany(x => x.RequestedBatches)
             .WithOne()
             .HasForeignKey(b => b.OrderId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderToSupplierBatch context
        builder.Entity<OrderToSupplierBatch>(b =>
        {
            b.HasKey(x => new { x.OrderId, x.BatchId });

            b.Property(x => x.OrderId)
             .HasColumnName("order_id")
             .IsRequired();

            b.Property(x => x.BatchId)
             .HasColumnName("batch_id")
             .IsRequired();

            b.Property(x => x.Quantity)
             .HasColumnName("quantity")
             .IsRequired();

            b.Property(x => x.Accepted)
             .HasColumnName("accepted")
             .IsRequired();
            

        });

        // CustomSupply context
        builder.Entity<CustomSupply>(cs =>
        {
            // Clave primaria
            cs.HasKey(x => x.Id);
            cs.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            // FK a Supply
            cs.Property(x => x.SupplyId)
                .HasColumnName("supply_id")
                .IsRequired();
            cs.HasOne(x => x.Supply)
                .WithMany()
                .HasForeignKey(x => x.SupplyId);

            // Description
            cs.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(300)
                .IsRequired();

            // MinStock / MaxStock
            cs.Property(x => x.MinStock)
                .HasColumnName("min_stock")
                .IsRequired();
            cs.Property(x => x.MaxStock)
                .HasColumnName("max_stock")
                .IsRequired();
 

            // Price
            cs.Property(x => x.Price)
                .HasColumnName("price")
                .HasPrecision(18, 2)
                .IsRequired();

            // UserId
            cs.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();
        });
        
        // Batch context
        builder.Entity<Batch>(b =>
        { 
            b.HasKey(x => x.Id);
            b.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();      
 
            b.Property(x => x.CustomSupplyId)
                .HasColumnName("custom_supply_id")
                .IsRequired();

            b.HasOne(x => x.CustomSupply)
                .WithMany()  
                .HasForeignKey(x => x.CustomSupplyId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.Stock)
                .HasColumnName("stock")
                .IsRequired();

            b.Property(x => x.ExpirationDate)
                .HasColumnName("expiration_date")
                .IsRequired(false);

            b.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

        });
        
        // Supply context
        builder.Entity<Supply>(s =>
        {
            s.HasKey(x => x.Id);
            s.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            s.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            s.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(300)
                .IsRequired(false);

            s.Property(x => x.Perishable)
                .HasColumnName("perishable")
                .IsRequired();

            s.Property(x => x.UnitName)
                .HasColumnName("unit_name")
                .HasMaxLength(50)
                .IsRequired();

            s.Property(x => x.UnitAbbreviation)
                .HasColumnName("unit_abbreviation")
                .HasMaxLength(10)
                .IsRequired();

            s.Property(x => x.Category)
                .HasColumnName("category")
                .HasMaxLength(100)
                .IsRequired();
        });
        
    } 
}