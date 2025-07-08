using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Services;

namespace Restock.Platform.API.Profiles.Application.Internal.QueryServices;
 
public class BusinessCategoryQueryService(
    IBusinessCategoryRepository businessCategoryRepository): IBusinessCategoryQueryService
{
    public async Task<IEnumerable<BusinessCategory?>> Handle(GetAllBusinessCategoriesQuery query)
    {
        return await businessCategoryRepository.ListAsync();
    }
}