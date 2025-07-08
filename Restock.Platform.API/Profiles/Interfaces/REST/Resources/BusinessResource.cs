namespace Restock.Platform.API.Profiles.Interfaces.REST.Resources;

public record BusinessResource(
    int Id,
    string Name,
    string Email,
    string Phone,
    string Address,
    string Categories);