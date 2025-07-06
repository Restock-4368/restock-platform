namespace Restock.Platform.API.Profiles.Interfaces.ACL;

public interface IBusinessesContextFacade
{
    Task<int> CreateBusiness(
        string name, 
        string email,
        string phone,
        string address, 
        string categories);

    Task DeleteBusiness(int businessId);
}