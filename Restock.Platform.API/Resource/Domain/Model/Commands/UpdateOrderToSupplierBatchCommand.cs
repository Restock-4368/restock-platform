namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateOrderToSupplierBatchCommand(int OrderId, int BatchId, double Quantity, bool Accepted);