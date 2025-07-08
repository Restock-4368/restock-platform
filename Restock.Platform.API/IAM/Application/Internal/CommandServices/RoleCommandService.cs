using Restock.Platform.API.IAM.Domain.Model.Commands;
using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Domain.Model.ValueObjects;
using Restock.Platform.API.IAM.Domain.Repositories;
using Restock.Platform.API.IAM.Domain.Services;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.IAM.Application.Internal.CommandServices;

public class RoleCommandService( IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    : IRoleCommandService
{
    public async Task Handle(SeedRolesCommand command)
    {
        foreach (var role in Enum.GetValues<ERoles>())
        {  
            var roleValue = (ERoles)role;

            var exists = await roleRepository.ExistsByNameAsync(roleValue);
            if (!exists)
            { 
                var newRole = new Role(roleValue);
                await roleRepository.AddAsync(newRole);
            }
        }

        await unitOfWork.CompleteAsync();
    } 
}