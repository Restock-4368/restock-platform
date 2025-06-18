namespace Restock.Platform.API.Planning.Interfaces.REST.Resources;

public record AddRecipeSupplyResource(
    Guid SupplyId,
    double Quantity);