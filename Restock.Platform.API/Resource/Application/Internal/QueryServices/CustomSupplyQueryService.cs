using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;

namespace Restock.Platform.API.Resource.Application.Internal.QueryServices;

public class CustomSupplyQueryService(ICustomSupplyRepository customSupplyRepository) : ICustomSupplyQueryService
{
    public async Task<CustomSupply?> Handle(GetCustomSupplyByIdQuery query)
    {
        return await customSupplyRepository.FindByIdAsync(query.CustomSupplyId);
    }

    public async Task<IEnumerable<CustomSupply?>> Handle(GetAllCustomSuppliesQuery query)
    {
        return await customSupplyRepository.ListAsync();
    }
}