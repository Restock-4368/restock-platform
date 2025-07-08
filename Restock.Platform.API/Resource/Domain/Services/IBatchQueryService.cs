using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface IBatchQueryService
{
    Task<Batch?> Handle(GetBatchByIdQuery query);
    Task<IEnumerable<Batch?>> Handle(GetAllBatchesQuery query);
}