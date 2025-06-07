namespace Restock.Platform.API.Planning.Domain.Model.Entities;

using Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public partial class Recipe
{
    public int Id { get; private set; }
    public RecipeName Name { get; private set; }
    public string? Description { get; private set; }

    private readonly List<Ingredient> _ingredients = new();
    public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();

    public Recipe(RecipeName name, string? description = null)
    {
        Name = name;
        Description = description;
    }
}