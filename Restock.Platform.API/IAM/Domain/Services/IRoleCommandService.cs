using Restock.Platform.API.IAM.Domain.Model.Commands;

namespace Restock.Platform.API.IAM.Domain.Services;

public interface IRoleCommandService
{
    Task Handle(SeedRolesCommand command);
}