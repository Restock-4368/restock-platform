 
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<OrderToSupplier>
{  
    Task<IEnumerable<OrderToSupplier>> ListAsyncWithRequestedBatches();
    Task<OrderToSupplier> FindByIdAsyncWithRequestedBatches(int id);
    
    Task<bool> ExistsByBatchId(int batchId);
}