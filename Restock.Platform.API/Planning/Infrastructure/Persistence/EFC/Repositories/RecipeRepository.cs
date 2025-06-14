using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Planning.Infrastructure.Persistence.EFC.Repositories;

public class RecipeRepository(AppDbContext context) : BaseRepository<Recipe>(context), IRecipeRepository
{
    public async Task<Recipe?> FindByIdAsync(Guid id)
    {
        return await Context.Set<Recipe>()
            .Include("_supplies")
            .FirstOrDefaultAsync(r => r.Id.Value == id);
    }

    public async Task<IEnumerable<Recipe>> ListAsync()
    {
        return await Context.Set<Recipe>()
            .Include("_supplies")
            .ToListAsync();
    }
}