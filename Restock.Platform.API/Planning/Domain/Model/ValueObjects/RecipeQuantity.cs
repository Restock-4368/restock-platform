namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipeQuantity(double Value)
{
    public RecipeQuantity () : this(0.0)
    {
        if (Value <= 0) throw new ArgumentOutOfRangeException("Quantity must be positive.", nameof(Value));
    }
}