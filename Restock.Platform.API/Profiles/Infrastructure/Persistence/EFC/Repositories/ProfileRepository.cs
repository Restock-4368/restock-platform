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

    public async Task<Profile?> FindByIdWithBusinessAsync(int id)
    {
        return await Context.Set<Profile>()
            .Include(b => b.Business)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Profile>> ListWithBusinessAsync()
    {
        return await Context.Set<Profile>()
            .Include(b => b.Business)
            .ToListAsync();
    } 
}