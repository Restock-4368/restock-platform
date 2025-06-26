 
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public class OrderToSupplierBatchResourceFromEntityAssembler
{
    public static OrderToSupplierBatchResource ToResourceFromEntity(OrderToSupplierBatch orderToSupplierBatch)
    {
        return new OrderToSupplierBatchResource(
            orderToSupplierBatch.OrderToSupplierId,  
            orderToSupplierBatch.BatchId,  
            orderToSupplierBatch.Quantity,
            orderToSupplierBatch.Accepted
        );
    }
}
 
