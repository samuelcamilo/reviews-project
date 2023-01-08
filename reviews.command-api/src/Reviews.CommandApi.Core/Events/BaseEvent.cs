namespace Reviews.CommandApi.Core.Events;

public abstract record BaseEvent
{
    public DateTime DateOccurred { get; protected set; }
}

