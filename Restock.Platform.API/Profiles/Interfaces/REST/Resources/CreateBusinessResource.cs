namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record CreateBusinessResource(
    string Name,
    string Email, 
    string Phone,
    string Address, 
    string Categories);