namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateBatchResource( 
    int Stock, 
    DateTime? ExpirationDate);