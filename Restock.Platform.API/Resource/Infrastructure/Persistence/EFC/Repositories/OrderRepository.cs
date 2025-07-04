using Microsoft.EntityFrameworkCore;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;

public class OrderRepository(AppDbContext context) : BaseRepository<OrderToSupplier>(context), IOrderRepository
{
    public async Task<IEnumerable<OrderToSupplier>> ListAsyncWithRequestedBatches()
    {
        return await context.OrdersToSupplier
            .Include(o => o.RequestedBatches)
            .ToListAsync(); 
    }
    public async Task<OrderToSupplier> FindByIdAsyncWithRequestedBatches(int id)
    {
        return await Context.OrdersToSupplier
            .Include(o => o.RequestedBatches)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}