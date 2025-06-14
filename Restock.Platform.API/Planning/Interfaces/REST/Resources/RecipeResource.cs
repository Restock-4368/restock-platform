namespace Restock.Platform.API.Planning.Interfaces.REST.Resources;

public record RecipeSupplyResource(
    Guid SupplyId,
    double Quantity
);

public record RecipeResource(
    Guid Id,
    string Name,
    string Description,
    string ImageUrl,
    decimal TotalPrice,
    int UserId,
    List<RecipeSupplyResource> Supplies
);