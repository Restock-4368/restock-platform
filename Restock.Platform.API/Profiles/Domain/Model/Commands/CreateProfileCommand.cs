namespace Restock.Platform.API.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Avatar,
    string Email,
    string Phone,
    string Address, 
    string Country,
    int UserId,
    int BusinessId
);