namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record AddOrderRequestedBatchCommand(Guid OrderId, int BatchId, double Quantity, bool Accepted);