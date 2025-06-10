namespace Restock.Platform.API.Planning.Domain.Model.Entities;

using Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public class Supply
{
    public int Id { get; private set; }
    public IngredientName Name { get; private set; }
    public IngredientQuantity Quantity { get; private set; }

    public Supply(IngredientName name, IngredientQuantity quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}