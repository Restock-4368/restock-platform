using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Planning.Domain.Model.Entities;

public class RecipeSupply
{
    public SupplyIdentifier SupplyId { get; init; }
    public RecipeQuantity Quantity { get; private set; }
    
    public RecipeSupply(SupplyIdentifier supplyId, RecipeQuantity quantity)
    {
        SupplyId = supplyId;
        Quantity = quantity;
    }
    
    public void UpdateQuantity(RecipeQuantity newQuantity)
    {
        Quantity = newQuantity;
    }
}