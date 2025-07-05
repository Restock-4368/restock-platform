using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class CustomSupplyResourceFromEntityAssembler                      
{                                                                         
    public static CustomSupplyResource ToResourceFromEntity(CustomSupply? customSupply)         
    {                                                                     
        return new CustomSupplyResource(     
            customSupply.Id,
            customSupply.SupplyId,
            customSupply.Supply != null ? SupplyResourceFromEntityAssembler.ToResourceFromEntity(customSupply.Supply) : null,
            customSupply.Description, 
            customSupply.MinStock,
            customSupply.MaxStock, 
            customSupply.Price,
            customSupply.UserId                               
        );                                                            
    }                                                                     
}                                     