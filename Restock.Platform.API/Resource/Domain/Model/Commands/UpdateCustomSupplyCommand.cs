namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateCustomSupplyCommand(    
    int CustomSupplyId,
    string Description, 
    int MinStock, 
    int MaxStock,   
    decimal Price);