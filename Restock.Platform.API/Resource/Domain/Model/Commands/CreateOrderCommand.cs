namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record CreateOrderCommand(
    DateTime Date,
    DateTime EstimatedShipDate,
    DateTime EstimatedShipTime,
    string Description,
    int AdminRestaurantId,
    int SupplierId, 
    int RequestedProductsCount,
    decimal TotalPrice );