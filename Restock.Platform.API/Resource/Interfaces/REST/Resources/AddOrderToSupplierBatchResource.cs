namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record AddOrderToSupplierBatchResource(
    int BatchId,
    double Quantity);