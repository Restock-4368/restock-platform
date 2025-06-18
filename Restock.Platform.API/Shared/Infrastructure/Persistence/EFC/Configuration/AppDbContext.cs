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
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
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

        // ========== Recipe ==========
        builder.Entity<Recipe>(recipe =>
        {
            recipe.HasKey(r => r.Id);
            recipe.Property(r => r.Id)
                .HasConversion(id => id.Value, value => new RecipeIdentifier(value))
                .ValueGeneratedNever(); 

            recipe.Property(r => r.Name).IsRequired().HasMaxLength(100);
            recipe.Property(r => r.Description).IsRequired().HasMaxLength(300);
            recipe.Property(r => r.UserId).IsRequired();

            recipe.Property(r => r.ImageUrl)
                .HasConversion(img => img.Value, value => new RecipeImageURL(value))
                .HasColumnName("image_url")
                .IsRequired();

            recipe.Property(r => r.TotalPrice)
                .HasConversion(p => p.Value, value => new RecipePrice(value))
                .HasColumnName("total_price")
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
                .HasConversion(rid => rid.Value, value => new RecipeIdentifier(value))
                .HasColumnName("recipe_id");

            rs.Property(r => r.SupplyId)
                .HasConversion(sid => sid.Value, value => new SupplyIdentifier(value))
                .HasColumnName("supply_id");

            rs.Property(r => r.Quantity)
                .HasConversion(q => q.Value, value => new RecipeQuantity(value))
                .HasColumnName("quantity");
            
        });
        
        builder.UseSnakeCaseNamingConvention();

    }

}

