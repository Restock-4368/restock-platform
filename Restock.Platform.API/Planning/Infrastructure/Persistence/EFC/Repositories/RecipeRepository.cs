using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Model.Entities;
using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Planning.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Planning.Infrastructure.Persistence.EFC.Repositories;

public class RecipeRepository(AppDbContext context) : BaseRepository<Recipe>(context), IRecipeRepository
{
    public async Task<Recipe?> FindByIdAsync(Guid id, bool includeSupplies = false)
    {
        var query = Context.Set<Recipe>().AsQueryable();
        if (includeSupplies)
            query = query.Include(r => r.Supplies);
        
        return await Context.Set<Recipe>()
            .Include(r => r.Supplies)
            .FirstOrDefaultAsync(r => r.Id == new RecipeIdentifier(id));
    }
    
    public async Task<IEnumerable<Recipe>> ListAsync(bool includeSupplies = false)
    {
        var query = Context.Set<Recipe>().AsQueryable();
        if (includeSupplies)
            query = query.Include(r => r.Supplies);

        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<RecipeSupply>> ListSuppliesByRecipeIdAsync(Guid recipeId)
    {
        return await Context.Set<RecipeSupply>()
            .Where(rs => rs.RecipeId == new RecipeIdentifier(recipeId))
            .ToListAsync();
    }
}