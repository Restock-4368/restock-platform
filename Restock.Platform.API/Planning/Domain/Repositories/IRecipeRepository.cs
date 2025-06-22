using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Model.Entities;

namespace Restock.Platform.API.Planning.Domain.Repositories;

public interface IRecipeRepository
{
    Task<Recipe?> FindByIdAsync(Guid id, bool includeSupplies = false);
    Task<IEnumerable<Recipe>> ListAsync(bool includeSupplies = false);
    Task<IEnumerable<RecipeSupply>> ListSuppliesByRecipeIdAsync(Guid recipeId);
    Task AddAsync(Recipe recipe);
    void Update(Recipe recipe);
    void Remove(Recipe recipe);
}