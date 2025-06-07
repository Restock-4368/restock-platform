namespace Restock.Platform.API.Planning.Domain.Model.Entities;

public partial class Recipe
{
    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        _ingredients.Remove(ingredient);
    }
}