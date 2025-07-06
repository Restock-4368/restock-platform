using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Services;

namespace Restock.Platform.API.Profiles.Application.Internal.QueryServices;

public class BusinessQueryService(
    IBusinessRepository businessRepository): IBusinessQueryService
{
    public async Task<IEnumerable<Business?>> Handle(GetAllBusinessesQuery query)
    {
        return await businessRepository.ListAsync();
    }

    public async Task<Business?> Handle(GetBusinessByIdQuery query)
    {
        return await businessRepository.FindByIdAsync(query.BusinessId);
    } 
}