namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record struct IngredientQuantity(decimal Amount, string Unit)
{
    public override string ToString() => $"{Amount} {Unit}";
}