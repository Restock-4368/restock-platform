using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;

namespace Restock.Platform.API.Resource.Application.Internal.QueryServices;

public class SupplyQueryService(ISupplyRepository supplyRepository) : ISupplyQueryService
{
    public async Task<Supply?> Handle(GetSupplyByIdQuery query)
    {
        return await supplyRepository.FindByIdAsync(query.SupplyId);
    }

    public async Task<IEnumerable<Supply?>> Handle(GetAllSuppliesQuery query)
    {
        return await supplyRepository.ListAsync();
    }
}