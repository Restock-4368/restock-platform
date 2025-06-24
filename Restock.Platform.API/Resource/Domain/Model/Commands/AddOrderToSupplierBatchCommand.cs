namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record AddOrderToSupplierBatchCommand(int OrderId, int BatchId, double Quantity, bool Accepted);