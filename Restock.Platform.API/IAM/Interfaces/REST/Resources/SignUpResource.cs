﻿namespace Restock.Platform.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password, int RoleId);