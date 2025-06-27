using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public class UpdateOrderToSupplierBatchCommandFromResourceAssembler
{
    public static UpdateOrderToSupplierBatchCommand ToCommandFromResource(
        int orderId,
        UpdateOrderToSupplierBatchResource orderToSupplierBatchResource)
    {
        return new UpdateOrderToSupplierBatchCommand(
            orderId,
            orderToSupplierBatchResource.BatchId,
            orderToSupplierBatchResource.Quantity,
            orderToSupplierBatchResource.Accepted
        );
    }
}