using Restock.Platform.API.IAM.Application.Internal.OutboundServices;
using Restock.Platform.API.IAM.Application.Internal.OutboundServices.ACL;
using Restock.Platform.API.IAM.Domain.Model.Aggregates;
using Restock.Platform.API.IAM.Domain.Model.Commands;
using Restock.Platform.API.IAM.Domain.Repositories;
using Restock.Platform.API.IAM.Domain.Services;
using Restock.Platform.API.Shared.Domain.Exceptions;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    ExternalProfilesService externalProfilesService)
    : IUserCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
    
        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new BusinessRuleException("Invalid username or password");
         
        var token = tokenService.GenerateToken(user);
        
        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new BusinessRuleException($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword, command.RoleId);
         
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            
            var businessId = await externalProfilesService.CreateBusiness();
            await externalProfilesService.CreateProfile(user.Id, businessId);
        }
        catch (Exception e)
        {
            throw new BusinessRuleException($"An error occurred while creating user: {e.Message}");
        }
    }
}