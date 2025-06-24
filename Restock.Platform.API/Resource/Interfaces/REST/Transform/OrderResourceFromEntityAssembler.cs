using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class OrderResourceFromEntityAssembler
{
    public static OrderResource ToResourceFromEntity(OrderToSupplier order)
    {
        return new OrderResource(
            order.OrderId, 
            order.Date, 
            order.EstimatedShipDate,
            order.EstimatedShipTime,
            order.Description, 
            order.AdminRestaurantId, 
            order.SupplierId,
            order.RequestedProductsCount, 
            order.TotalPrice,
            order.RequestedBatches
                .Select(b => new OrderToSupplierBatchResource(
                    b.OrderToSupplierId, 
                    b.BatchId,  
                    b.Quantity,
                    b.Accepted
                    ))
                .ToList());
    }
}


//IMPLEMENTAR: 
// public static class BatchResourceAssembler
// {
//     public static BatchResource ToResource(Batch batch) =>
//         new(batch.BatchId, batch.SupplyId, batch.Stock, batch.ExpirationDate, batch.UserId);
// }
//
// public static class SupplyResourceAssembler
// {
//     public static SupplyResource ToResource(Supply supply) =>
//         new(supply.Id, supply.Name, supply.Description, supply.Perishable,
//             supply.MinStock, supply.MaxStock, supply.CategoryId, supply.UnitMeasurementId,
//             supply.Price, supply.UserId);
// }