using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;

public class OrderRepository(AppDbContext context) : BaseRepository<OrderToSupplier>(context), IOrderRepository
{
    public new async Task<IEnumerable<OrderToSupplier>> ListAsync()
    {
        return await Context.Set<OrderToSupplier>()
            .ToListAsync();
    }
}