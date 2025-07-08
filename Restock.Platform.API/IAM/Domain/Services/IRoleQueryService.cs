using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Domain.Model.Queries;

namespace Restock.Platform.API.IAM.Domain.Services;

public interface IRoleQueryService
{
    Task<Role?> Handle(GetRoleByIdQuery query);
 
    Task<IEnumerable<Role>> Handle(GetAllRolesQuery query);
}