namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipeIdentifier(Guid Value)
{
    public RecipeIdentifier() : this(Guid.NewGuid()) {}

}