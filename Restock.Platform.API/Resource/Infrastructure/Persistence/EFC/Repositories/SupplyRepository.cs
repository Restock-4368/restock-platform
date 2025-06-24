using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;

public class SupplyRepository(AppDbContext context)
    : BaseRepository<Supply>(context), ISupplyRepository
{
    public async Task<IEnumerable<Supply>> ListByIdsAsync(IEnumerable<int> supplyIds)
    {
        return await Context.Set<Supply>()
            .Where(s => supplyIds.Contains(s.Id))
            .ToListAsync();
    }
}