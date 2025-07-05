namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateOrderToSupplierBatchCommand(
    int BatchId,
    int OrderId,
    double Quantity);