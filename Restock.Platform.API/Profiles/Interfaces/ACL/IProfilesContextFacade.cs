namespace Restock.Platform.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<int> CreateProfile(
        string firstName,
        string lastName,
        string avatar,
        string email,
        string phone,
        string address, 
        string country,
        int userId,
        int businessId);
    
    Task DeleteProfile(int profileId);
    
    Task<int> FetchProfileIdByEmail(string email);
    
    Task<int> CreateBusiness(
        string name, 
        string email,
        string phone,
        string address, 
        string categories);

    Task DeleteBusiness(int businessId);

}