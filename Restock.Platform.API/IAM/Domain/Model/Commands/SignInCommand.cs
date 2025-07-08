using System.Text.Json.Serialization;

namespace Restock.Platform.API.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign in command
 * </summary>
 * <remarks>
 *     This command object includes the username and password to sign in
 * </remarks>
 */
public record SignInCommand(
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("password")] string Password
);