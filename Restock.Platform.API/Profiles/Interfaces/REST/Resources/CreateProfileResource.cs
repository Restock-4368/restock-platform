namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    string FirstName,
    string LastName,
    string Avatar,
    string Email, 
    string Phone,
    string Address, 
    string Country,
    int UserId,
    int BusinessId);