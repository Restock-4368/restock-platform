namespace Restock.Platform.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(
    int Id, 
    string Username, 
    string Token,
    int RoleId);