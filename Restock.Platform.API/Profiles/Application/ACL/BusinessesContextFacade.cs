using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.ACL;

namespace Restock.Platform.API.Profiles.Application.ACL;

public class BusinessesContextFacade(
    IBusinessCommandService businessCommandService,
    IBusinessQueryService businessQueryService)
    : IBusinessesContextFacade
{
    public async Task<int> CreateBusiness(string name, string email, string phone, string address, string categories)
    {
        var createBusinessCommand = new CreateBusinessCommand(
            name,
            email,
            phone,
            address,
            categories);
        var business = await businessCommandService.Handle(createBusinessCommand);
        return business?.Id ?? 0;
    }

    public async Task DeleteBusiness(int businessId)
    {
        var command = new DeleteBusinessCommand(businessId);
        await businessCommandService.Handle(command);
    }
}
 