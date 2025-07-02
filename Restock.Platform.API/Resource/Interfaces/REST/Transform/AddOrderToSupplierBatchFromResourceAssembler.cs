using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class AddOrderToSupplierBatchFromResourceAssembler
{
    public static AddOrderToSupplierBatchCommand ToCommandFromResource(AddOrderToSupplierBatchResource resource)
    {
        return new AddOrderToSupplierBatchCommand(resource.OrderId, resource.BatchId, resource.Quantity);
    }
}