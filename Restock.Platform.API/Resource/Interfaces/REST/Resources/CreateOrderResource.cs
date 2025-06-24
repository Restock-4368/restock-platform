namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record CreateOrderResource(
    DateTime Date,
    DateTime? EstimatedShipDate,
    DateTime? EstimatedShipTime,
    string? Description,
    int AdminRestaurantId,
    int SupplierId, 
    int RequestedProductsCount,
    decimal TotalPrice );