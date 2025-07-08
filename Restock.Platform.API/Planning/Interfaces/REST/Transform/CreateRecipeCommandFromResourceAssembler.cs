using Restock.Platform.API.Planning.Domain.Model.Commands;
using Restock.Platform.API.Planning.Interfaces.REST.Resources;

namespace Restock.Platform.API.Planning.Interfaces.REST.Transform;

public static class CreateRecipeCommandFromResourceAssembler
{
    public static CreateRecipeCommand ToCommand(CreateRecipeResource resource)
    {
        return new CreateRecipeCommand(
            resource.Name,
            resource.Description,
            resource.ImageUrl,
            resource.TotalPrice,
            resource.UserId
        );
    }
}