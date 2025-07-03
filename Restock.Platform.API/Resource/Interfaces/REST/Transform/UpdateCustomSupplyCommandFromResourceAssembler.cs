using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public class UpdateCustomSupplyCommandFromResourceAssembler
{
    public static UpdateCustomSupplyCommand ToCommandFromResource(
        int customSupplyId,
        UpdateCustomSupplyResource customSupplyResource)
    {
        return new UpdateCustomSupplyCommand( 
            customSupplyId,
            customSupplyResource.Description,
            customSupplyResource.Perishable,
            customSupplyResource.MinStock,
            customSupplyResource.MaxStock,
            customSupplyResource.Price
        );
        
    }
}