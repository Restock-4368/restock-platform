using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Domain.Model.Queries;
using Restock.Platform.API.IAM.Domain.Repositories;
using Restock.Platform.API.IAM.Domain.Services;

namespace Restock.Platform.API.IAM.Application.Internal.QueryServices;
 
public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
{ 
    
    public async Task<Role?> Handle(GetRoleByIdQuery query)
    {
        return await roleRepository.FindByIdAsync(query.Id);
    }
 
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query)
    {
        return await roleRepository.ListAsync();
    }
 
}