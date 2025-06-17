namespace Restock.Platform.API.Planning.Domain.Model.Commands;

public record SupplyInput(Guid SupplyId, double Quantity);

public record CreateRecipeCommand(
    string Name, 
    string Description, 
    string ImageUrl, 
    decimal TotalPrice, 
    int UserId,
    IEnumerable<SupplyInput> Supplies);
