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
            .Where(s => customSupplyIds.Contains(s.CustomSupplyId))
            .ToListAsync();
    }
}