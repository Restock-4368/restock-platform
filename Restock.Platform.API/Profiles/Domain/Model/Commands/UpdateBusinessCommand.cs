namespace Restock.Platform.API.Profiles.Domain.Model.Commands;

public record UpdateBusinessCommand(
    int BusinessId,
    string Name,
    string Email,
    string Phone,
    string Address, 
    string Categories);