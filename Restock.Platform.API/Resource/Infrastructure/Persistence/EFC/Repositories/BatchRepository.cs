using Microsoft.EntityFrameworkCore; 
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;

public class BatchRepository(AppDbContext context) : BaseRepository<Batch>(context), IBatchRepository
{
    public new async Task<IEnumerable<Batch>> ListAsync()
    {
        return await Context.Set<Batch>()
            .Include(b => b.CustomSupply)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Batch>> ListByIdsAsync(IEnumerable<int> batchIds)
    {
        return await Context.Set<Batch>()
            .Where(b => batchIds.Contains(b.Id))
            .Include(b => b.CustomSupply)
            .ToListAsync();
    }

    public async Task<bool> ExistsBySupplyIdAndUserIdAsync(int supplyId, int userId)
    {
        return await Context.Set<Batch>()
            .Include(b => b.CustomSupply)
            .AnyAsync(b => b.CustomSupply.SupplyId == supplyId && b.UserId == userId);
    }

    public async Task<IEnumerable<Batch>> ListByCustomSupplyId(int customSupplyId)
    {
        return await Context.Set<Batch>()
            .Where(b => b.CustomSupplyId == customSupplyId)
            .Include(b => b.CustomSupply)
            .ToListAsync();
    }

    public async Task<IEnumerable<Batch>> ListWithCustomSupplyAsync()
    {
        return await Context.Set<Batch>()
            .Include(b => b.CustomSupply)
                .ThenInclude(cs => cs.Supply)
            .ToListAsync();
    }

    public async Task<Batch?> FindByIdWithCustomSupplyAsync(int id)
    {
        return await Context.Set<Batch>()
            .Include(b => b.CustomSupply)
                .ThenInclude(cs => cs.Supply)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}