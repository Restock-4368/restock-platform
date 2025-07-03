namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateBatchResource(
    int BatchId, 
    int Stock, 
    DateTime? ExpirationDate);