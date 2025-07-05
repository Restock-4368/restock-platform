namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record CreateCustomSupplyCommand(
    int SupplyId, 
    string Description,
    int MinStock, 
    int MaxStock,  
    decimal Price, 
    int UserId);