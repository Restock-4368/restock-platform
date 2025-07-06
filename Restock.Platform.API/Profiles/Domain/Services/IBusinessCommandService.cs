using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Entities;

namespace Restock.Platform.API.Profiles.Domain.Services;
 
public interface IBusinessCommandService
{
    Task<Business?> Handle(CreateBusinessCommand command);
    
    Task Handle(UpdateBusinessCommand command);
    
    Task Handle(DeleteBusinessCommand command);
}