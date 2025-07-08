using Restock.Platform.API.Planning.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class CreateOrderCommandFromResourceAssembler
{
    public static CreateOrderCommand ToCommandFromResource(CreateOrderResource resource)
    {
        return new CreateOrderCommand(   
            resource.Description, 
            resource.AdminRestaurantId, 
            resource.SupplierId
        );
    }
}