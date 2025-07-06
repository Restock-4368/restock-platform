 
using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Shared.Domain.Exceptions;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Profiles.Application.Internal.CommandServices;
 
public class BusinessCommandService(
    IBusinessRepository businessRepository,
    IUnitOfWork unitOfWork): IBusinessCommandService
{
    public async Task<Business?> Handle(CreateBusinessCommand command)
    {
        var business = new Business(command);
        try
        {
            await businessRepository.AddAsync(business);
            await unitOfWork.CompleteAsync();
            return business;
        } catch (Exception ex)
        {
            // Log the exception (not implemented here)
            Console.WriteLine($"Error creating business: {ex.Message}");
            return null;
        }
    }

    public async Task Handle(UpdateBusinessCommand command)
    {
        var business = await businessRepository.FindByIdAsync(command.BusinessId);

        if (business is null)
            throw new BusinessRuleException("Business not found");
 
        business.Update(
            command.Name,
            command.Email,
            command.Phone,
            command.Address,
            command.Categories
        );

        businessRepository.Update(business);
        await unitOfWork.CompleteAsync();
    } 
    
    public async Task Handle(DeleteBusinessCommand command)
    {
        var business = await businessRepository.FindByIdAsync(command.BusinessId);

        if (business is null)
            throw new BusinessRuleException("Business not found");

        businessRepository.Remove(business);
        await unitOfWork.CompleteAsync();
    }
}