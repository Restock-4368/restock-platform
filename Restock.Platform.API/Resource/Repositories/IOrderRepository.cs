using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Repositories;

public interface IOrderRepository
{
    Task<OrderToSupplier?> FindByIdAsync(Guid id, bool includeRequestedBatches = false);
    Task<IEnumerable<OrderToSupplier>> ListAsync(bool includeSupplies = false);
    Task<IEnumerable<OrderToSupplierBatch>> ListOrderToSupplierBatchesByOrderIdAsync(Guid orderId);
    Task AddAsync(OrderToSupplier order);
    void Update(OrderToSupplier order);
    void Remove(OrderToSupplier order);
}