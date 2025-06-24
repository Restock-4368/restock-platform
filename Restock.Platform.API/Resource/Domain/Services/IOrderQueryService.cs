using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface IOrderQueryService
{
    Task<OrderToSupplier?> Handle(GetOrderByIdQuery query);
    Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersQuery query);
    Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersBySupplierIdQuery query);
    Task<IEnumerable<OrderToSupplierBatch>> Handle(GetOrderToSupplierBatchesByOrderIdQuery query);
    
    Task<OrderToSupplierBatch?> Handle(GetOrderToSupplierBatchByIdQuery query);
    
    Task<IEnumerable<Batch>> Handle(GetOrderBatchesByOrderIdQuery byOrderIdQuery);
    Task<IEnumerable<Supply>> Handle(GetOrderSuppliesByOrderIdQuery byOrderIdQuery);
}