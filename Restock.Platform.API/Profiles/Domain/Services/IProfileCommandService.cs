using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Commands;

namespace Restock.Platform.API.Profiles.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
    
    Task Handle(UpdateProfileCommand command);
    
    Task Handle(DeleteProfileCommand command);
}