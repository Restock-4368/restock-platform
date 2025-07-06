namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record UpdateBusinessResource(
    string Id, 
    string Name,
    string Email, 
    string Phone,
    string Address, 
    string Categories);