using Restock.Platform.API.Resource.Domain.Model.Aggregates; 
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Domain.Repositories;

public interface ICustomSupplyRepository : IBaseRepository<CustomSupply>
{
    Task<IEnumerable<CustomSupply>> ListByIdsAsync(IEnumerable<int> customSupplyIds);
    
    Task<bool> ExistsBySupplyIdAndUserIdAsync(int supplyId, int userId);
    
    Task<CustomSupply?> FindByIdWithSupplyAsync(int id);
    
    Task<IEnumerable<CustomSupply>> ListWithSupplyAsync();
}