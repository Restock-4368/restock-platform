using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class UpdateOrderToSupplierBatchCommandFromResourceAssembler
{
    public static UpdateOrderToSupplierBatchCommand ToCommandFromResource(
        int orderId,
        int batchId,
        UpdateOrderToSupplierBatchResource orderToSupplierBatchResource)
    {
        return new UpdateOrderToSupplierBatchCommand(  
            orderId,
            batchId,
            orderToSupplierBatchResource.Quantity
        );
    }
}