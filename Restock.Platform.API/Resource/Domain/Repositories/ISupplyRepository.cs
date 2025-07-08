 
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Domain.Repositories;

public interface ISupplyRepository : IBaseRepository<Supply>
{
    Task<IEnumerable<Supply>> ListByIdsAsync(IEnumerable<int> supplyIds);
}