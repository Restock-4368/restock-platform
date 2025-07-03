namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record CreateCustomSupplyResource(
    int SupplyId, 
    string Description, 
    bool Perishable, 
    int MinStock, 
    int MaxStock, 
    int CategoryId, 
    decimal Price, 
    int UserId);