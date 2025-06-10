using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Planning.Domain.Model.Aggregates;

using Restock.Platform.API.Planning.Domain.Model.Entities;

public class RecipeAggregate
{
    public RecipeIdentifier Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public RecipeImageURL ImageUrl { get; private set; }
    public RecipePrice TotalPrice { get; private set; }
    // public UserId UserId { get; private set; }
    public List<RecipeSupply> Supplies { get; private set; }

    public RecipeAggregate(RecipeIdentifier id, string name, string description, RecipeImageURL imageUrl, RecipePrice totalPrice/*, UserId userId*/)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        TotalPrice = totalPrice;
        // UserId = userId;
        Supplies = new List<RecipeSupply>();
    }

    public void AddSupply(SupplyIdentifier supplyId, RecipeQuantity quantity)
    {
        if (Supplies.Any(s => s.SupplyId == supplyId)) return;
        Supplies.Add(new RecipeSupply(supplyId, quantity));
    }

    public void RemoveSupply(SupplyIdentifier supplyId)
    {
        var item = Supplies.FirstOrDefault(s => s.SupplyId == supplyId);
        if (item != null) Supplies.Remove(item);
    }
}