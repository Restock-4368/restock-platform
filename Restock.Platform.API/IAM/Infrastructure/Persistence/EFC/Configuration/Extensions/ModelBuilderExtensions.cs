using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.IAM.Domain.Model.Aggregates;
using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Domain.Model.ValueObjects;

namespace Restock.Platform.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.RoleId).IsRequired();
            
        //Roles
        builder.Entity<Role>().HasKey(r => r.Id);
        builder.Entity<Role>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd(); 
        builder.Entity<Role>()
            .Property(p => p.Name)
            .HasConversion(
                v => v.ToString(),              // Enum → string
                v => Enum.Parse<ERoles>(v))     // string → Enum
            .HasColumnName("Name");
        
    }
}