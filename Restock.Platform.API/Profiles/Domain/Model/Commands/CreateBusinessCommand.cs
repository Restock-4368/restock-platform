namespace Restock.Platform.API.Profiles.Domain.Model.Commands;

public record CreateBusinessCommand(
    string Name,
    string Email,
    string Phone,
    string Address, 
    string Categories
    ); 