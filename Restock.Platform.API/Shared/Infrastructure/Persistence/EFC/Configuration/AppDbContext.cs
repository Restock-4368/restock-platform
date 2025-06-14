using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Model.Entities;
using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeSupply> RecipeSupplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseSnakeCaseNamingConvention();

        // ---------------- RECIPE ----------------
        builder.Entity<Recipe>(recipe =>
        {
            recipe.HasKey("_id");
            recipe.Ignore(r => r.Supplies); 

            // Mapear value object RecipeIdentifier
            recipe.OwnsOne(r => r.Id, id =>
            {
                id.Property(p => p.Value).HasColumnName("id");
            });

            recipe.Property(r => r.Name).IsRequired().HasMaxLength(100);
            recipe.Property(r => r.Description).IsRequired().HasMaxLength(300);
            recipe.Property(r => r.UserId).IsRequired();

            // Mapear value object RecipeImageURL
            recipe.OwnsOne(r => r.ImageUrl, img =>
            {
                img.Property(p => p.Value).HasColumnName("image_url").IsRequired();
            });

            // Mapear value object RecipePrice
            recipe.OwnsOne(r => r.TotalPrice, price =>
            {
                price.Property(p => p.Value).HasColumnName("total_price").IsRequired();
            });

            // Relación con RecipeSupply
            recipe.HasMany(typeof(RecipeSupply), "_supplies")
                  .WithOne()
                  .HasForeignKey("recipe_id")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ---------------- RECIPE SUPPLY ----------------
        builder.Entity<RecipeSupply>(rs =>
        {
            rs.HasKey("recipe_id", "supply_id"); // Clave compuesta

            // Mapear value object RecipeId
            rs.OwnsOne(r => r.RecipeId, rid =>
            {
                rid.Property(p => p.Value).HasColumnName("recipe_id");
            });

            // Mapear value object SupplyId
            rs.OwnsOne(r => r.SupplyId, sid =>
            {
                sid.Property(p => p.Value).HasColumnName("supply_id");
            });

            // Mapear value object Quantity
            rs.OwnsOne(r => r.Quantity, qty =>
            {
                qty.Property(p => p.Value).HasColumnName("quantity");
            });
        });
    }
}

