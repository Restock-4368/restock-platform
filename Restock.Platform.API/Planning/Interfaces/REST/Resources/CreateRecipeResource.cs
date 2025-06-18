namespace Restock.Platform.API.Planning.Interfaces.REST.Resources;


public record CreateRecipeResource(
    string Name,
    string Description,
    string ImageUrl,
    decimal TotalPrice,
    int UserId
);