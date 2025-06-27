using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class SupplyResourceFromEntityAssembler                      
{                                                                         
    public static SupplyResource ToResourceFromEntity(Supply supply)         
    {                                                                     
        return new SupplyResource(                                         
            supply.Id,                                                
            supply.Name,                                         
            supply.Description,                                           
            supply.UnitMeasurement                                            
        );                                                            
    }                                                                     
}                                                                         