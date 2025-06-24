namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record AddOrderToSupplierBatchResource(int OrderId, int BatchId, double Quantity, bool Accepted);