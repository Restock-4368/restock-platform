using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;

namespace Restock.Platform.API.Resource.Application.Internal.QueryServices;

public class OrderQueryService(IOrderRepository orderRepository) : IOrderQueryService
{
    public async Task<OrderToSupplier?> Handle(GetOrderByIdQuery query)
    {
        return await orderRepository.FindByIdAsync(query.OrderId);
    }

    public async Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersBySupplierIdQuery query)
    {
        var allOrders = await orderRepository.ListAsync(); // o ListByAdminIdAsync si lo tenés
        return allOrders.Where(o => o.SupplierId == query.SupplierId);
    }
    
    public async Task<IEnumerable<OrderToSupplierBatch>> Handle(GetOrderToSupplierBatchesByOrderIdQuery query)
    {
        var order = await orderRepository.FindByIdAsync(query.OrderId);
        if (order is null) return null;

        return order.RequestedBatches; 
    }

    public Task<IEnumerable<Batch>> Handle(GetOrderBatchesQuery query)
    {
        //var order = await orderRepository.FindByIdAsync(query.OrderId);
        //if (order is null) return Enumerable.Empty<Batch>();
        //
        //// Debería haber una forma de acceder a los batches (desde otra capa o servicio)
        //var batchIds = order.RequestedBatches.Select(rb => rb.BatchId).ToList();
        //// Aquí debería consultar la base para obtener esos Batch. 
        //return await batchRepository.ListByIdsAsync(batchIds);
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Supply>> Handle(GetOrderSuppliesQuery query)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderToSupplierBatch?> Handle(GetOrderToSupplierBatchByIdQuery query)
    {
        var order = await orderRepository.FindByIdAsync(query.OrderId);
        if (order is null) return null;

        return order.RequestedBatches.FirstOrDefault(b => b.OrderToSupplierBatchId == query.OrderToSupplierBatchId);
    }
 
}