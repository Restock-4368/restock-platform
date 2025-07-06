using System.Text.Json;
using Restock.Platform.API.Profiles.Domain.Model.Entities; 
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Restock.Platform.API.Profiles.Infrastructure.Persistence.Seeders;
 
public static class BusinessCategorySeeder
{
    
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        
        if (context.BusinessCategories.Any())
            return;

        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Profiles", "Infrastructure", "Data", "businessCategories.json");

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File didn't found: {jsonFilePath}");

        var jsonData = await File.ReadAllTextAsync(jsonFilePath);
       
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var businessCategories = JsonSerializer.Deserialize<List<BusinessCategory>>(jsonData, options);

        
        if (businessCategories != null)
        {
            context.BusinessCategories.AddRange(businessCategories);
            await context.SaveChangesAsync();
        }
    }
}