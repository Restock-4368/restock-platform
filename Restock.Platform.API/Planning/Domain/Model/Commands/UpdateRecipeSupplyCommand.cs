namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record UpdateRecipeSupplyCommand(Guid RecipeId, int SupplyId, double Quantity);
