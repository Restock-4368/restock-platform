using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class UpdateOrderCommandFromResourceAssembler
{
    public static UpdateOrderCommand ToCommandFromResource(
        int orderId,
        UpdateOrderResource orderResource)
    {
        return new UpdateOrderCommand(  
            orderId,
            orderResource.EstimatedShipDate,
            orderResource.EstimatedShipTime,
            orderResource.Description
        );
    }
}