using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;

namespace Restock.Platform.API.Resource.Application.Internal.QueryServices;

public class OrderQueryService(
    IOrderRepository orderRepository,
    IBatchRepository batchRepository,
    ICustomSupplyRepository customSupplyRepository,
    ISupplyRepository supplyRepository) : IOrderQueryService
{
    public async Task<OrderToSupplier?> Handle(GetOrderByIdQuery query)
    {
        return await orderRepository.FindByIdAsyncWithRequestedBatches(query.OrderId);
    }

    public async Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersQuery query)
    {
        return await orderRepository.ListAsyncWithRequestedBatches();
    }

    public async Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersBySupplierIdQuery query)
    {
        var allOrders = await orderRepository.ListAsyncWithRequestedBatches(); 
        return allOrders.Where(o => o.SupplierId == query.SupplierId);
    }

    public async Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersByAdminRestaurantIdQuery query)
    {
        var allOrders = await orderRepository.ListAsyncWithRequestedBatches(); 
        return allOrders.Where(o => o.AdminRestaurantId == query.AdminRestaurantId);
    }

    public async Task<IEnumerable<OrderToSupplierBatch>> Handle(GetOrderToSupplierBatchesByOrderIdQuery query)
    {
        var order = await orderRepository.FindByIdAsyncWithRequestedBatches(query.OrderId);
        if (order is null) return Enumerable.Empty<OrderToSupplierBatch>();

        return order.RequestedBatches; 
    }

    public async Task<IEnumerable<Batch>> Handle(GetOrderBatchesByOrderIdQuery query)
    {
        var order = await orderRepository.FindByIdAsyncWithRequestedBatches(query.OrderId);
        if (order is null) return Enumerable.Empty<Batch>();

        var batchIds = order.RequestedBatches.Select(rb => rb.BatchId).Distinct().ToList();
        return await batchRepository.ListByIdsAsync(batchIds);
    }

    public async Task<IEnumerable<CustomSupply>> Handle(GetOrderCustomSuppliesByOrderIdQuery query)
    {
        var order = await orderRepository.FindByIdAsyncWithRequestedBatches(query.OrderId);
        if (order is null) return Enumerable.Empty<CustomSupply>();

        var batchIds = order.RequestedBatches.Select(rb => rb.BatchId).Distinct().ToList();
        var batches = await batchRepository.ListByIdsAsync(batchIds);

        var customSupplyIds = batches.Select(b => b.CustomSupplyId).Distinct().ToList();
        return await customSupplyRepository.ListByIdsAsync(customSupplyIds);
    }

    public async Task<IEnumerable<Supply>> Handle(GetOrderSuppliesByOrderIdQuery query)
    {
        var order = await orderRepository.FindByIdAsyncWithRequestedBatches(query.OrderId);
        if (order is null) return Enumerable.Empty<Supply>();

        var batchIds = order.RequestedBatches.Select(rb => rb.BatchId).Distinct().ToList();
        var batches = await batchRepository.ListByIdsAsync(batchIds);

        var customSupplyIds = batches.Select(b => b.CustomSupplyId).Distinct().ToList();
        var customSupplies = await customSupplyRepository.ListByIdsAsync(customSupplyIds);
        
        var suppliesIds = customSupplies.Select(b => b.SupplyId).Distinct().ToList();
        
        return await supplyRepository.ListByIdsAsync(suppliesIds);
    }
}