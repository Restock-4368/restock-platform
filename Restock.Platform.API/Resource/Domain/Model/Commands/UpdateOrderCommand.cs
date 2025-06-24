using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateOrderCommand(
    int OrderId,
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