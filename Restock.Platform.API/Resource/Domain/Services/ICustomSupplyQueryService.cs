using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.Queries;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface ICustomSupplyQueryService
{
    Task<CustomSupply?> Handle(GetCustomSupplyByIdQuery query);
    Task<IEnumerable<CustomSupply?>> Handle(GetAllCustomSuppliesQuery query);
}