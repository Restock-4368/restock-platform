 
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<OrderToSupplier>
{  
}