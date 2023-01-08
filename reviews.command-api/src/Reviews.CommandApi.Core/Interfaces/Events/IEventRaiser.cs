using Reviews.CommandApi.Core.Events;

namespace Reviews.CommandApi.Core.Interfaces.Events;

public interface IEventRaiser
{
    Task RaiseAsync<TEvent>(TEvent @event)
        where TEvent : BaseEvent;
}

