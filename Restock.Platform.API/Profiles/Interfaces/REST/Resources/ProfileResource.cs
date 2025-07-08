namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record ProfileResource(
    int Id,
    string FullName,
    string Avatar,
    string Email,
    string Phone,
    string Address,
    string Country, 
    int UserId,
    int BusinessId,
    BusinessResource Business);