namespace Restock.Platform.API.Resource.Domain.Model.Commands;


public record UpdateBatchCommand( 
    int BatchId,
    int Stock,
    DateTime? ExpirationDate);