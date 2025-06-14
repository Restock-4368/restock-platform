using Restock.Platform.API.Planning.Domain.Model.Aggregates;

namespace Restock.Platform.API.Planning.Domain.Services;

public interface IRecipeQueryService
{
    Task<IEnumerable<Recipe>> ListAsync();
    Task<Recipe?> GetByIdAsync(Guid id);
}