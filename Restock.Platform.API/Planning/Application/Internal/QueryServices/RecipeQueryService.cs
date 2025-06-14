using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Repositories;
using Restock.Platform.API.Planning.Domain.Services;

namespace Restock.Platform.API.Planning.Application.Internal.QueryServices;

public class RecipeQueryService(
    IRecipeRepository recipeRepository) : IRecipeQueryService
{
    public async Task<IEnumerable<Recipe>> ListAsync()
    {
        return await recipeRepository.ListAsync();
    }

    public async Task<Recipe?> GetByIdAsync(Guid id)
    {
        return await recipeRepository.FindByIdAsync(id);
    }
}