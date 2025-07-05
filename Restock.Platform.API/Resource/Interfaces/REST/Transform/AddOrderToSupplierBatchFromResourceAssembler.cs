using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class AddOrderToSupplierBatchFromResourceAssembler
{
    public static AddOrderToSupplierBatchCommand ToCommandFromResource(
        int orderId, 
        AddOrderToSupplierBatchResource resource)
    {
        return new AddOrderToSupplierBatchCommand(
            orderId,
            resource.BatchId,
            resource.Quantity);
    }
}