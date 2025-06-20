namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record DeleteRecipeSupplyCommand(Guid RecipeId, int SupplyId);