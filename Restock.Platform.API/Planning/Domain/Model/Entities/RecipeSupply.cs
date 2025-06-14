using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Planning.Domain.Model.Entities;

public class RecipeSupply
{
    public RecipeIdentifier RecipeId { get; init; }
    public SupplyIdentifier SupplyId { get; init; }
    public RecipeQuantity Quantity { get; private set; }

    protected RecipeSupply() { } 

    public RecipeSupply(RecipeIdentifier recipeId, SupplyIdentifier supplyId, RecipeQuantity quantity)
    {
        RecipeId = recipeId;
        SupplyId = supplyId;
        Quantity = quantity;
    }
    
    public void UpdateQuantity(RecipeQuantity newQuantity)
    {
        Quantity = newQuantity;
    }
}