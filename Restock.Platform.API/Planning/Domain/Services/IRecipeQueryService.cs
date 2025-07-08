using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Model.Entities;

namespace Restock.Platform.API.Planning.Domain.Services;

public interface IRecipeQueryService
{
    Task<IEnumerable<Recipe>> ListAsync(bool includeSupplies = false);
    Task<Recipe?> GetByIdAsync(Guid id, bool includeSupplies = false);
    Task<IEnumerable<RecipeSupply>> ListSuppliesByRecipeIdAsync(Guid recipeId);
}