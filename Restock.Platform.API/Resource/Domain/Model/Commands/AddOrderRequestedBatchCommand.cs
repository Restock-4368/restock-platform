namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record AddOrderRequestedBatchCommand(int OrderId, int BatchId, double Quantity, bool Accepted);