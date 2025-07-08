 
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Domain.Repositories;

public interface IBatchRepository : IBaseRepository<Batch>
{
    Task<IEnumerable<Batch>> ListByIdsAsync(IEnumerable<int> batchIds);
    
    Task<bool> ExistsBySupplyIdAndUserIdAsync(int supplyId, int userId);

    Task<IEnumerable<Batch>> ListByCustomSupplyId(int customSupplyId);
    
    Task<IEnumerable<Batch>> ListWithCustomSupplyAsync();
    
    Task<Batch?> FindByIdWithCustomSupplyAsync(int id);
}
