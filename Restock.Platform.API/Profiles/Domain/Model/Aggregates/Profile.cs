﻿using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Profiles.Domain.Model.Aggregates;

public class Profile
{
    public int Id { get; }
    public PersonName Name { get; private set; }
    
    public Avatar Avatar { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    
    public string Phone { get; private set; } 
    
    public string Country { get; private set; } 
    
    public UserId UserId { get; private set; }
    
    public int BusinessId { get; private set; }
    
    public Business Business { get; set; } 
    
    public string FullName => Name.FullName; 

    public Profile(){}
    
    public Profile(int userId, int businessId)
    {
        UserId = new UserId(userId);
        BusinessId = businessId;
        Avatar = new Avatar();
        Name = new PersonName();
        Email = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
        Country = string.Empty;
    } 
    
    public Profile(string firstName, string lastName, string avatar, string email, string phone, string address, string country, int userId, int businessId)
    {
        Name = new PersonName(firstName, lastName);
        Email = email;
        Address = address;
        Phone = phone;
        Country = country;
        UserId = new UserId(userId);
        BusinessId = businessId;
        Avatar = new Avatar(avatar);
    }

    public Profile(CreateProfileCommand command) : this( 
        command.UserId,
        command.BusinessId)
    {
        
    }
    
    public void Update(string? firstName, string? lastName, string? avatar, string? email, 
        string? phone, string? address, string? country)
    {   
        if(firstName != null && lastName != null)
            Name = new PersonName(firstName, lastName);
        
        Avatar = avatar != null ? new Avatar(avatar) : Avatar;
        Email = email ?? Email;
        Address = address ?? Address; 
        Phone = phone ?? Phone; 
        Country = country ?? Country; 
    }
}