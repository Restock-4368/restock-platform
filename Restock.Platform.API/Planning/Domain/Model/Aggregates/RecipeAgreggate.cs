namespace Restock.Platform.API.Planning.Domain.Model.Aggregates;

using Restock.Platform.API.Planning.Domain.Model.Entities;

public class RecipeAggregate
{
    public Recipe Root { get; }

    public RecipeAggregate(Recipe recipe)
    {
        Root = recipe;
    }
}