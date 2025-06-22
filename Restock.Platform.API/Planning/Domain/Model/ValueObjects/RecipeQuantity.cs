namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipeQuantity
{
    public double Value { get; init; }

    public RecipeQuantity()
    {
        Value = 0.0;
    }
    public RecipeQuantity(double value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Quantity must be positive.");
        Value = value;
    }
}