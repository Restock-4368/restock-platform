using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface ISupplyQueryService
{
    Task<Supply?> Handle(GetSupplyByIdQuery query);
    Task<IEnumerable<Supply?>> Handle(GetAllSuppliesQuery query);
}