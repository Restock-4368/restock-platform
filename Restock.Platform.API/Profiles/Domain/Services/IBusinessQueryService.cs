using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.Queries;

namespace Restock.Platform.API.Profiles.Domain.Services;
 
public interface IBusinessQueryService
{
    Task<IEnumerable<Business?>> Handle(GetAllBusinessesQuery query);
    Task<Business?> Handle(GetBusinessByIdQuery query); 
}