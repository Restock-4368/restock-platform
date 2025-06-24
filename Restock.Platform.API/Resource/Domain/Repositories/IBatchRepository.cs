 
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Domain.Repositories;

public interface IBatchRepository : IBaseRepository<Batch>
{
    Task<IEnumerable<Batch>> ListByIdsAsync(IEnumerable<int> batchIds);
}
