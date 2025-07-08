using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
 
public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    { 
        // ---- Profile ----
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().OwnsOne(p => p.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(n => n.FirstName).HasColumnName("FirstName");
            n.Property(n => n.LastName).HasColumnName("LastName");
        });
        builder.Entity<Profile>().Property(p => p.Address).HasColumnName("Address");
        builder.Entity<Profile>().Property(p => p.Email).HasColumnName("EmailAddress");
        builder.Entity<Profile>().Property(p => p.Phone).HasColumnName("Phone");
        builder.Entity<Profile>().Property(p => p.Country).HasColumnName("Country");
        builder.Entity<Profile>()
            .Property(p => p.Avatar)
            .HasConversion(
                v => v.Value,
                v => new Avatar(v))
            .HasColumnName("Avatar");
        builder.Entity<Profile>()
            .Property(p => p.UserId)
            .HasConversion(
                v => v.Value,
                v => new UserId(v))
            .HasColumnName("UserId")
            .IsRequired();
        builder.Entity<Profile>().Property(p => p.BusinessId).HasColumnName("BusinessId").IsRequired();
        
        // ---- Business ----
        builder.Entity<Business>().HasKey(b => b.Id);
        builder.Entity<Business>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Business>().Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Entity<Business>().Property(b => b.Email).HasColumnName("EmailAddress");
        builder.Entity<Business>().Property(b => b.Address).HasColumnName("Address");
        builder.Entity<Business>().Property(b => b.Phone).HasColumnName("Phone");
        builder.Entity<Business>().Property(b => b.Categories).HasColumnName("Categories");
        
    }
}