namespace Restock.Platform.API.Resource.Domain.Model.Commands;


public record UpdateBatchCommand(
    Guid Id,
    int SupplyId,
    double Stock,
    DateTime ExpirationDate,
    int UserId);