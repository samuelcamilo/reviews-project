using Reviews.CommandApi.Core.Events;

namespace Reviews.CommandApi.Core.Interfaces.Events;

public interface IEventHandler<in TEvent>
    where TEvent : BaseEvent
{
    Task HandlerAsync(TEvent @event);
}

