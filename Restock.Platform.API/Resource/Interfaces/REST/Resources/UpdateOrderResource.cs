namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateOrderResource(    
    int OrderId,
    DateTime Date,
    DateTime? EstimatedShipDate,
    DateTime? EstimatedShipTime,
    string? Description,
    int AdminRestaurantId,
    int SupplierId,
    int RequestedProductsCount,
    decimal TotalPrice,
    bool PartiallyAccepted,
    List<OrderToSupplierBatchResource> RequestedBatches
);

 
 