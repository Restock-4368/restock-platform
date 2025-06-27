using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record OrderResource(
    int Id,
    DateTime Date,
    DateTime? EstimatedShipDate,
    DateTime? EstimatedShipTime,
    string? Description,
    int AdminRestaurantId,
    int SupplierId,
    int RequestedProductsCount,
    decimal TotalPrice,
    List<OrderToSupplierBatchResource> RequestedBatches
    );
