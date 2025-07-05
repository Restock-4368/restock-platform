using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;

namespace Restock.Platform.API.Resource.Application.Internal.QueryServices;

public class BatchQueryService(IBatchRepository batchRepository) : IBatchQueryService
{
    public async Task<Batch?> Handle(GetBatchByIdQuery query)
    {
        return await batchRepository.FindByIdWithCustomSupplyAsync(query.BatchId);
    }

    public async Task<IEnumerable<Batch?>> Handle(GetAllBatchesQuery query)
    {
        return await batchRepository.ListWithCustomSupplyAsync();
    }
}