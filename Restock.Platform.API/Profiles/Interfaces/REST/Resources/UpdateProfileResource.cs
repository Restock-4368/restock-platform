namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record UpdateProfileResource(
    string Id, 
    string FirstName,
    string LastName,
    string Avatar,
    string Email, 
    string Phone,
    string Address, 
    string Country)
{
    public string EstimatedShipTime { get; set; }
}