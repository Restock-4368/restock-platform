using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Restock.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Restock.Platform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class BusinessCategoryRepository(AppDbContext context) : BaseRepository<BusinessCategory>(context), IBusinessCategoryRepository
{
    
}