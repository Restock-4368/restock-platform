using System.Text.Json;
using Restock.Platform.API.Resource.Domain.Model.Entities; 
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.Seeders;

public static class SupplySeeder
{
    
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        
        if (context.Supplies.Any())
            return;

        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "Infrastructure", "Data", "supplies.json");

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File didn't found: {jsonFilePath}");

        var jsonData = await File.ReadAllTextAsync(jsonFilePath);
       
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var supplies = JsonSerializer.Deserialize<List<Supply>>(jsonData, options);

        
        if (supplies != null)
        {
            context.Supplies.AddRange(supplies);
            await context.SaveChangesAsync();
        }
    }
}