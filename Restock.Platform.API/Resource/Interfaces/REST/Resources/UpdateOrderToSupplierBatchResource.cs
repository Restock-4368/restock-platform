namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateOrderToSupplierBatchResource(int OrderId, int BatchId, double Quantity, bool Accepted);