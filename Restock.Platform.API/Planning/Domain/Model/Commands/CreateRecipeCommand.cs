namespace Restock.Platform.API.Planning.Domain.Model.Commands;

using Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record CreateRecipeCommand(string Name, string Description, string ImageUrl, decimal TotalPrice, int UserId);
