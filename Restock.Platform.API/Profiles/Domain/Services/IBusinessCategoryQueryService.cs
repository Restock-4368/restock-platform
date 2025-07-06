 
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.Queries;

namespace Restock.Platform.API.Profiles.Domain.Services;

public interface IBusinessCategoryQueryService
{ 
    Task<IEnumerable<BusinessCategory?>> Handle(GetAllBusinessCategoriesQuery query);
}