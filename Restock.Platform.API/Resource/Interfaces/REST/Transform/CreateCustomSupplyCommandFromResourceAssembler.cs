using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public class CreateCustomSupplyCommandFromResourceAssembler
{
    public static CreateCustomSupplyCommand ToCommandFromResource(CreateCustomSupplyResource resource)
    {
        return new CreateCustomSupplyCommand(   
            resource.SupplyId,
            resource.Description,
            resource.Perishable,
            resource.MinStock,
            resource.MaxStock,
            resource.CategoryId,
            resource.Price,
            resource.UserId 
        );
    }
}