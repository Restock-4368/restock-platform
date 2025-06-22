namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateOrderRequestedBatchCommand(Guid OrderId, int BatchId, double Quantity, bool Accepted);