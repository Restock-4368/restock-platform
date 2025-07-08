namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    int UserId,
    int BusinessId);