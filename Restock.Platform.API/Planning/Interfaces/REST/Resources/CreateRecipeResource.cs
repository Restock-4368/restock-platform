namespace Restock.Platform.API.Planning.Interfaces.REST.Resources;

public record SupplyInputResource(Guid SupplyId, double Quantity);

public record CreateRecipeResource(
    string Name,
    string Description,
    string ImageUrl,
    decimal TotalPrice,
    int UserId,
    List<SupplyInputResource> Supplies
);