using Restock.Platform.API.IAM.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Repositories;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Shared.Domain.Exceptions;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IBusinessRepository businessRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork): IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
            throw new BusinessRuleException("User not found.");

        var business = await businessRepository.FindByIdAsync(command.BusinessId);
        if (business == null)
            throw new BusinessRuleException("Business not found.");
        
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception ex)
        {
            // Log the exception (not implemented here)
            Console.WriteLine($"Error creating profile: {ex.Message}");
            return null;
        }
    }

    public async Task Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);

        if (profile is null)
            throw new BusinessRuleException("Profile not found");
 
        profile.Update(
            command.FirstName,
            command.LastName,
            command.Avatar,
            command.Email,
            command.Phone,
            command.Address,
            command.Country
        );

        profileRepository.Update(profile);
        await unitOfWork.CompleteAsync();
        
    }
 
    public async Task Handle(DeleteProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);

        if (profile is null)
            throw new BusinessRuleException("Profile not found");

        profileRepository.Remove(profile);
        await unitOfWork.CompleteAsync();
    }
}