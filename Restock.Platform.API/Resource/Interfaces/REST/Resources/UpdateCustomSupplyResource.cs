namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateCustomSupplyResource(   
    string Description, 
    int MinStock, 
    int MaxStock,   
    decimal Price);