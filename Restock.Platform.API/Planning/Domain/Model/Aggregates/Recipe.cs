using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Planning.Domain.Model.Aggregates;

using Restock.Platform.API.Planning.Domain.Model.Entities;

public class Recipe
{
    public RecipeIdentifier Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public RecipeImageURL ImageUrl { get; private set; }
    public RecipePrice TotalPrice { get; private set; }
    public int UserId { get; private set; }
   
    private readonly List<RecipeSupply> _supplies = new();
    public IReadOnlyCollection<RecipeSupply> Supplies => _supplies.AsReadOnly();
    
    protected Recipe() {}
    
    public Recipe(RecipeIdentifier id, string name, string description, RecipeImageURL imageUrl, RecipePrice totalPrice, int userId)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        TotalPrice = totalPrice;
        UserId = userId;
    }

    public void AddSupply(RecipeIdentifier recipeId, SupplyIdentifier supplyId, RecipeQuantity quantity)
    {

        var existing = _supplies.FirstOrDefault(s => s.SupplyId == supplyId);

        if (existing != null) throw new InvalidOperationException("Supply already added to recipe");

        _supplies.Add(new RecipeSupply(recipeId, supplyId, quantity));
    }
    
    public void RemoveSupply(SupplyIdentifier supplyId)
    {
        var supply = _supplies.FirstOrDefault(s => s.SupplyId == supplyId);
        if (supply != null) _supplies.Remove(supply);
    }
    
    public void Update(string name, string description, RecipeImageURL imageUrl, RecipePrice totalPrice)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        TotalPrice = totalPrice;
    }
    
    public void ReplaceSupplies(IEnumerable<(RecipeIdentifier recipeId, SupplyIdentifier supplyId, RecipeQuantity quantity)> newSupplies)
    {
        _supplies.Clear();
        foreach (var (recipeId, supplyId, quantity) in newSupplies)
        {
            AddSupply(recipeId, supplyId, quantity);
        }
    }

}