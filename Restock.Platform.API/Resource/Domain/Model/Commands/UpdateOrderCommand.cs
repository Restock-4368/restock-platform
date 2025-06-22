namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateOrderCommand(
    Guid Id,
    DateTime Date,
    DateTime EstimatedShipDate,
    DateTime EstimatedShipTime,
    string Description,
    int AdminRestaurantId,
    int SupplierId, 
    int RequestedProductsCount,
    decimal TotalPrice,
    bool PartiallyAccepted
); 