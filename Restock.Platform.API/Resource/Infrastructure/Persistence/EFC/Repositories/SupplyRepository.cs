  
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Resource.Infrastructure.Persistence.EFC.Repositories;

public class SupplyRepository(AppDbContext context)
    : BaseRepository<Supply>(context), ISupplyRepository
{
    
}