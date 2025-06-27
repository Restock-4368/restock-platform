using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class CustomSupplyResourceFromEntityAssembler                      
{                                                                         
    public static CustomSupplyResource ToResourceFromEntity(CustomSupply customSupply)         
    {                                                                     
        return new CustomSupplyResource(     
            customSupply.Id,
            customSupply.SupplyId,
            customSupply.Supply,
            customSupply.Description,
            customSupply.Perishable,
            customSupply.MinStock,
            customSupply.MaxStock,
            customSupply.CategoryId,
            customSupply.Price,
            customSupply.UserId                               
        );                                                            
    }                                                                     
}                                     