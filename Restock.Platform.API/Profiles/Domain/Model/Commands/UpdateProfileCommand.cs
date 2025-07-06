﻿namespace Restock.Platform.API.Profiles.Domain.Model.Commands;

public record UpdateProfileCommand(
    int ProfileId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Address, 
    string Country
    );