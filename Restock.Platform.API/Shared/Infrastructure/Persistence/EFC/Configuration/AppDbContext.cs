using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Model.Entities;
using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Planning
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeSupply> RecipeSupplies { get; set; }

    // Resource 
    public DbSet<OrderToSupplier> OrdersToSupplier { get; set; }
    public DbSet<OrderToSupplierBatch> OrderToSupplierBatches { get; set; }
    public DbSet<CustomSupply> CustomSupplies { get; set; }
    public DbSet<Batch> Batches { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ========== Recipe ==========
        builder.Entity<Recipe>(recipe =>
        {
            recipe.HasKey(r => r.Id);
            recipe.Property(r => r.Id)
                .HasConversion(id => id.Value, value => new RecipeIdentifier(value))
                .ValueGeneratedNever(); 

            recipe.Property(r => r.Name).IsRequired().HasMaxLength(100);
            recipe.Property(r => r.Description).IsRequired(false).HasMaxLength(300);
            recipe.Property(r => r.UserId).IsRequired();

            recipe.Property(r => r.ImageUrl)
                .HasColumnName("image_url")
                .HasConversion(img => img.Value, value => new RecipeImageURL(value))
                .IsRequired();

            recipe.Property(r => r.TotalPrice)
                .HasColumnName("total_price")
                .HasConversion(p => p.Value, value => new RecipePrice(value))
                .IsRequired();

            recipe.Navigation(r => r.Supplies)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            recipe.HasMany(r => r.Supplies)
                .WithOne(rs => rs.Recipe)
                .HasForeignKey(rs => rs.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== RecipeSupply ==========
        builder.Entity<RecipeSupply>(rs =>
        {
            rs.HasKey(r => new { r.RecipeId, r.SupplyId });

            rs.Property(r => r.RecipeId)
                .HasColumnName("recipe_id")
                .HasConversion(rid => rid.Value, value => new RecipeIdentifier(value));

            rs.Property(r => r.SupplyId)
                .HasColumnName("supply_id")
                .HasConversion(sid => sid.Value, value => new SupplyIdentifier(value));

            rs.Property(r => r.Quantity)
                .HasColumnName("quantity")
                .HasConversion(q => q.Value, value => new RecipeQuantity(value));

        });
        
        // ========== Resource mappings ==========

        // OrderToSupplier
        builder.Entity<OrderToSupplier>(o =>
        {
            o.HasKey(x => x.Id);
            o.Property(x => x.Id)
             .HasConversion(id => id, value => value)
             .ValueGeneratedOnAdd();

            o.Property(x => x.Date)
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
             .HasForeignKey(b => b.OrderToSupplierId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderToSupplierBatch
        builder.Entity<OrderToSupplierBatch>(b =>
        {
            b.HasKey(x => new { x.OrderToSupplierId, x.BatchId });

            b.Property(x => x.OrderToSupplierId)
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

        // CustomSupply
        builder.Entity<CustomSupply>(cs =>
        {
            // Clave primaria
            cs.HasKey(x => x.Id);
            cs.Property(x => x.Id)
                .HasColumnName("custom_supply_id")
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

            // Perishable
            cs.Property(x => x.Perishable)
                .HasColumnName("perishable")
                .IsRequired();

            // MinStock / MaxStock
            cs.Property(x => x.MinStock)
                .HasColumnName("min_stock")
                .IsRequired();
            cs.Property(x => x.MaxStock)
                .HasColumnName("max_stock")
                .IsRequired();

            // CategoryId
            cs.Property(x => x.CategoryId)
                .HasColumnName("category_id")
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
        
        // Batch
        builder.Entity<Batch>(b =>
        { 
            b.HasKey(x => x.Id);
            b.Property(x => x.Id)
                .HasColumnName("batch_id")
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
        builder.UseSnakeCaseNamingConvention();

    }

}

