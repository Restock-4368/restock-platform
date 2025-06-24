using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface IOrderQueryService
{
    Task<OrderToSupplier?> Handle(GetOrderByIdQuery query);
    Task<IEnumerable<OrderToSupplier>> Handle(GetAllOrdersByUserIdQuery idQuery);
    Task<IEnumerable<OrderToSupplierBatch>> Handle(GetOrderToSupplierBatchesByOrderIdQuery query);
    
    Task<OrderToSupplierBatch?> Handle(GetOrderToSupplierBatchByIdQuery query);
    
    Task<IEnumerable<Batch>> Handle(GetOrderBatchesQuery query);
    Task<IEnumerable<Supply>> Handle(GetOrderSuppliesQuery query);
}