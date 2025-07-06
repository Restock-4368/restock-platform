namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record UpdateBusinessResource( 
    string Name,
    string Email, 
    string Phone,
    string Address, 
    string Categories);