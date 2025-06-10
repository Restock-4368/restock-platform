namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record AddSupplyToRecipeCommand(Guid RecipeId, int SupplyId, double Quantity);
