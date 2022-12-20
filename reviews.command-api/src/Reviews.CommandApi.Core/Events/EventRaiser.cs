using Microsoft.Extensions.DependencyInjection;
using Reviews.CommandApi.Core.Interfaces.Events;

namespace Reviews.CommandApi.Core.Events;

internal class EventRaiser : IEventRaiser
{
    private readonly IServiceProvider _provider;

    public EventRaiser(IServiceProvider provider) =>
        _provider = provider;

    public async Task RaiseAsync<TEvent>(TEvent @event) 
        where TEvent : BaseEvent
    {
        var handler = (IEventHandler<TEvent>)_provider
            .GetRequiredService(typeof(IEventHandler<TEvent>));

        if (handler is not null)
        {
            await handler.HandlerAsync(@event);
        }
    }
}

