using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Shared.Domain.Exceptions;

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
         
        var business = businessRepository.FindByIdAsync(query.BusinessId);
        
        if (business == null)
            throw new BusinessRuleException($"Business with ID {query.BusinessId} not found.");
        
        return await business;
    } 
}