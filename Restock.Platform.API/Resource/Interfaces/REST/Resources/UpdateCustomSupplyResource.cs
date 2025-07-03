namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateCustomSupplyResource(   
    int CustomSupplyId,
    string Description, 
    bool Perishable, 
    int MinStock, 
    int MaxStock,   
    decimal Price);