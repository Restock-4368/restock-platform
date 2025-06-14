using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Interfaces.REST.Resources;

namespace Restock.Platform.API.Planning.Interfaces.REST.Transform;

public static class RecipeResourceFromEntityAssembler
{
    public static RecipeResource ToResource(Recipe recipe)
    {
        return new RecipeResource(
            recipe.Id.Value,
            recipe.Name,
            recipe.Description,
            recipe.ImageUrl.Value,
            recipe.TotalPrice.Value,
            recipe.UserId,
            recipe.Supplies.Select(s => new RecipeSupplyResource(
                s.SupplyId.Value,
                s.Quantity.Value
            )).ToList()
        );
    }
}