namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record OrderToSupplierBatchResource(int OrderId, int BatchId, double Quantity, bool Accepted);