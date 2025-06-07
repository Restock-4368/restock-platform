namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record IngredientName(string Value)
{
    public override string ToString() => Value;
}