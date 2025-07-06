using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Queries;

namespace Restock.Platform.API.Profiles.Domain.Services;
 
public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    Task<Profile?> Handle(GetProfileByIdQuery query); 
    
    Task<Profile?> Handle(GetProfileByEmailQuery query);
}