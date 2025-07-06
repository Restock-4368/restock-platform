using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<Profile?> FindByEmailAsync(string email)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.Email == email);
    }
}