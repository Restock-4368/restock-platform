namespace Restock.Platform.API.Planning.Domain.Model.Entities;

using Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public class Ingredient
{
    public int Id { get; private set; }
    public IngredientName Name { get; private set; }
    public IngredientQuantity Quantity { get; private set; }

    public Ingredient(IngredientName name, IngredientQuantity quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}