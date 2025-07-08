namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record UpdateRecipeCommand(
    Guid Id,
    string Name,
    string Description,
    string ImageUrl,
    decimal TotalPrice
);