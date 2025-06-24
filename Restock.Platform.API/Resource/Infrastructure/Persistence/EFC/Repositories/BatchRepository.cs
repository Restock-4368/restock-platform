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
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Batch>> ListByIdsAsync(IEnumerable<int> batchIds)
    {
        return await Context.Set<Batch>()
            .Where(b => batchIds.Contains(b.BatchId))
            .ToListAsync();
    }
}