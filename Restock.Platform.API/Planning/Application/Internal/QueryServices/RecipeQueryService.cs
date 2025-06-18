using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Repositories;
using Restock.Platform.API.Planning.Domain.Services;
using Restock.Platform.API.Planning.Domain.Model.Entities;

namespace Restock.Platform.API.Planning.Application.Internal.QueryServices;

public class RecipeQueryService(
    IRecipeRepository recipeRepository) : IRecipeQueryService
{
    public async Task<IEnumerable<Recipe>> ListAsync(bool includeSupplies = false)
    {
        return await recipeRepository.ListAsync(includeSupplies);
    }

    public async Task<Recipe?> GetByIdAsync(Guid id, bool includeSupplies = false)
    {
        return await recipeRepository.FindByIdAsync(id, includeSupplies);
    }

    public async Task<IEnumerable<RecipeSupply>> ListSuppliesByRecipeIdAsync(Guid recipeId)
    {
        return await recipeRepository.ListSuppliesByRecipeIdAsync(recipeId);
    }
}