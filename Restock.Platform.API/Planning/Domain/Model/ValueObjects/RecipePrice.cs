namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipePrice
{
    public decimal Value { get; init; }

    public RecipePrice()
    {
        Value = 0.0m;
    }

    public RecipePrice(decimal value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.");
        Value = value;
    }
}