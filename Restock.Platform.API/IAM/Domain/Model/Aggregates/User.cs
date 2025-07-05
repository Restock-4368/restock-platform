using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Restock.Platform.API.IAM.Domain.Model.ValueObjects;
using Restock.Platform.API.Shared.Domain.Exceptions;

namespace Restock.Platform.API.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
public partial class User
{
    public int Id { get; set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }

    // Foreign Key
    public int RoleId { get; private set; }

    // Navigation property
    [NotMapped]
    public ERoles Role { get; private set; }
    
 
    public User(string username, string hashedPassword, int roleId)
    {
        if (roleId != (int)ERoles.RestaurantSupplier && roleId != (int)ERoles.RestaurantAdministrator)
            throw new BusinessRuleException("RoleId must be 1 (RestaurantSupplier) or 2 (RestaurantAdministrator).");
        
        Username = username;
        PasswordHash = hashedPassword;
        RoleId = roleId;
        Role = (ERoles)roleId;
    }
    public User() { }
   
    
    /**
     * <summary>
     *     Update the username
     * </summary>
     * <param name="username">The new username</param>
     * <returns>The updated user</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}