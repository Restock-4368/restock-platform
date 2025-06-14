namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record SupplyInput(int SupplyId, int Quantity);

public record CreateRecipeCommand(
    string Name, 
    string Description, 
    string ImageUrl, 
    decimal TotalPrice, 
    int UserId,
    IEnumerable<SupplyInput> Supplies);
