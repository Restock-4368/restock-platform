namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record AddRecipeSupplyCommand(Guid RecipeId, int SupplyId, double Quantity);