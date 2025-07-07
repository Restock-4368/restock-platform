using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.IAM.Domain.Model.Aggregates;
using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Domain.Model.ValueObjects;
using Restock.Platform.API.IAM.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    public async Task<bool> ExistsByNameAsync(ERoles roleName)
    {
        return await Context.Set<Role>().AnyAsync(r => r.Name == roleName);
    }
}
 