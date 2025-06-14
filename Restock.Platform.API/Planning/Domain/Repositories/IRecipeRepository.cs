using Restock.Platform.API.Planning.Domain.Model.Aggregates;

namespace Restock.Platform.API.Planning.Domain.Repositories;

public interface IRecipeRepository
{
    Task<Recipe?> FindByIdAsync(Guid id);
    Task<IEnumerable<Recipe>> ListAsync();
    Task AddAsync(Recipe recipe);
    void Update(Recipe recipe);
    void Remove(Recipe recipe);
}