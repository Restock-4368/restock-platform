namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record CreateBatchCommand(int CustomSupplyId, int Stock, DateTime? ExpirationDate, int UserId);