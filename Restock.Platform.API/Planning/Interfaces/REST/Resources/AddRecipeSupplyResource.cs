namespace Restock.Platform.API.Planning.Interfaces.REST.Resources;

public record AddRecipeSupplyResource(
    int SupplyId,
    double Quantity);