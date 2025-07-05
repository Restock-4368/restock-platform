using Cortex.Mediator.Notifications;
using Restock.Platform.API.Shared.Domain.Model.Events;

namespace Restock.Platform.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}