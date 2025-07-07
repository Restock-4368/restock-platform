using Restock.Platform.API.IAM.Domain.Model.ValueObjects;

namespace Restock.Platform.API.IAM.Domain.Model.Entities;

public class Role
{
    public int Id { get; set; }
 
    public ERoles Name { get; set; }

    public Role() { }

    public Role(ERoles name)
    {
        Name = name;
    }
 
    /// <summary>
    /// Get the name of the role as a string.
    /// </summary>
    /// <returns>String name</returns>
    public string GetStringName()
    {
        return Name.ToString();
    }
 
    
    
    /// <summary>
    /// Get Role from name string.
    /// </summary>
    /// <param name="name">Role name</param>
    /// <returns>Role instance</returns>
    public static Role ToRoleFromName(string name)
    {
        return new Role(Enum.Parse<ERoles>(name));
    }
 
}