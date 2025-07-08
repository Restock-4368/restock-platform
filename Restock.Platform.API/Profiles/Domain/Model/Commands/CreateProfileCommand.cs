namespace Restock.Platform.API.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
    int UserId,
    int BusinessId
);