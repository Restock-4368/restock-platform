using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;

namespace Restock.Platform.API.Resource.Application.Internal.QueryServices;

public class OrderQueryService(IOrderRepository orderRepository,
    IBatchRepository batchRepository,
    ISupplyRepository supplyRepository) : IOrderQueryService
{
    public async Task<OrderToSupplier?> Handle(GetOrderByIdQuery query)
    {
        return await orderRepository.FindByIdAsync(query.OrderId);
    }

    public async Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersQuery query)
    {
        return await orderRepository.ListAsync();
    }

    public async Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersBySupplierIdQuery query)
    {
        var allOrders = await orderRepository.ListAsync(); // o ListByAdminIdAsync si lo tenés
        return allOrders.Where(o => o.SupplierId == query.SupplierId);
    }
    
    public async Task<IEnumerable<OrderToSupplierBatch>> Handle(GetOrderToSupplierBatchesByOrderIdQuery query)
    {
        var order = await orderRepository.FindByIdAsync(query.OrderId);
        if (order is null) return Enumerable.Empty<OrderToSupplierBatch>();

        return order.RequestedBatches; 
    }

    public async Task<IEnumerable<Batch>> Handle(GetOrderBatchesByOrderIdQuery byOrderIdQuery)
    {
        var order = await orderRepository.FindByIdAsync(byOrderIdQuery.OrderId);
        if (order is null) return Enumerable.Empty<Batch>();

        var batchIds = order.RequestedBatches.Select(rb => rb.BatchId).Distinct().ToList();
        return await batchRepository.ListByIdsAsync(batchIds);
    }

    public async Task<IEnumerable<Supply>> Handle(GetOrderSuppliesByOrderIdQuery byOrderIdQuery)
    {
        var order = await orderRepository.FindByIdAsync(byOrderIdQuery.OrderId);
        if (order is null) return Enumerable.Empty<Supply>();

        var batchIds = order.RequestedBatches.Select(rb => rb.BatchId).Distinct().ToList();
        var batches = await batchRepository.ListByIdsAsync(batchIds);

        var supplyIds = batches.Select(b => b.SupplyId).Distinct().ToList();
        return await supplyRepository.ListByIdsAsync(supplyIds);
    }
 
    public async Task<OrderToSupplierBatch?> Handle(GetOrderToSupplierBatchByIdQuery query)
    {
        var order = await orderRepository.FindByIdAsync(query.OrderId);
        if (order is null) return null;

        return order.RequestedBatches.FirstOrDefault(b => b.OrderToSupplierBatchId == query.OrderToSupplierBatchId);
    }
 
}