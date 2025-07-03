namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record CreateBatchResource(
    int CustomSupplyId, 
    int Stock, 
    DateTime? ExpirationDate, 
    int UserId);