 
using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Domain.Model.ValueObjects;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.IAM.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<bool> ExistsByNameAsync(ERoles roleName);
}
