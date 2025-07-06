using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByEmailAsync(string email);
}