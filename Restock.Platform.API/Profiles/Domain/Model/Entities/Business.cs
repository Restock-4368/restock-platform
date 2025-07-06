using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Profiles.Domain.Model.Entities;

public class Business
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    
    public string Phone { get; private set; } 
    
    public string Categories { get; private set; } 
     

    public Business()
    {
        Name = string.Empty;
        Email = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
        Categories = string.Empty;
    }
    
    public Business(string name, string email, string phone, string address, string categories)
    {
        Name = name;
        Email = email;
        Address = address;
        Phone = phone;
        Categories = categories;
    }

    public Business(CreateBusinessCommand command) : this(command.Name, command.Email, command.Phone, command.Address, command.Categories)
    {
        
    }
    
    public void Update(string? name, string? email, string? phone, 
        string? address, string? categories)
    {    
        Name = name ?? Name;
        Email = email ?? Email;
        Address = address ?? Address; 
        Phone = phone ?? Phone; 
        Categories = categories ?? Categories; 
    }
}