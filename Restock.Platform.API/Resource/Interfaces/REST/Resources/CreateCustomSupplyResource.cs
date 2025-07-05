namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record CreateCustomSupplyResource(
    int SupplyId, 
    string Description,  
    int MinStock, 
    int MaxStock, 
    decimal Price, 
    int UserId);