namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipePrice(decimal Value)
{
    public RecipePrice() : this(0.0m)
    {
        if (Value < 0) throw new ArgumentOutOfRangeException(nameof(Value), "Price cannot be negative.");
    }
}