using Reviews.CommandApi.Core.Constants;
using Reviews.CommandApi.Core.Interfaces.Events;
using Reviews.CommandApi.Core.Interfaces.Notifications;

namespace Reviews.CommandApi.Core.Events.ReviewRejected;

public class ReviewRejectedEventHandler : IEventHandler<ReviewRejectedEvent>
{
    private readonly INotification _notification;

    public ReviewRejectedEventHandler(INotification notification) =>
        _notification = notification;

    public async Task HandlerAsync(ReviewRejectedEvent @event)
    {
        var statusCode = @event.ReviewRejectedTypeId;
        var errorDescription = ResponseCodes.GetDescriptionTo(statusCode);

        _notification.Add(
            message: errorDescription,
            statusCode: statusCode);
    }
}

