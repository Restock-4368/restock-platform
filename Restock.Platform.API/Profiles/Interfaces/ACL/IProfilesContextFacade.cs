namespace Restock.Platform.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task CreateProfile(
        int userId,
        int businessId
        );
    
    Task DeleteProfile(int profileId);
    
    Task<int> FetchProfileIdByEmail(string email);
    
    Task<int> CreateBusiness();

    Task DeleteBusiness(int businessId);

}