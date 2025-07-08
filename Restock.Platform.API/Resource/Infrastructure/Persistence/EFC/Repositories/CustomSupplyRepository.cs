using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;

public class CustomSupplyRepository(AppDbContext context)
    : BaseRepository<CustomSupply>(context), ICustomSupplyRepository
{
    public async Task<IEnumerable<CustomSupply>> ListByIdsAsync(IEnumerable<int> customSupplyIds)
    {
        return await Context.Set<CustomSupply>()
            .Where(s => customSupplyIds.Contains(s.Id))
            .Include(c => c.Supply)
            .ToListAsync();
    }

    public async Task<bool> ExistsBySupplyIdAndUserIdAsync(int supplyId, int userId)
    {
        return await Context.Set<CustomSupply>().AnyAsync(batchRequest => (batchRequest.SupplyId == supplyId && batchRequest.UserId == userId));
    }

    public async Task<CustomSupply?> FindByIdWithSupplyAsync(int id)
    {
        return await Context.Set<CustomSupply>()
            .Include(b => b.Supply)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<CustomSupply>> ListWithSupplyAsync()
    {
        return await Context.Set<CustomSupply>()
            .Include(b => b.Supply)
            .ToListAsync();
    }
}