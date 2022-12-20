using Reviews.CommandApi.Core.Models.Requests;

namespace Reviews.CommandApi.Core.Events.ReviewRejected;

public record ReviewRejectedEvent : BaseEvent
{
    public Guid ReviewRejectedId { get; }
    public int ReviewRejectedTypeId { get; }
    public Exception ErrorReason { get; }

    public ReviewRejectedEvent(
        ReviewRequest request, 
        int responseCode, 
        Exception ex = null)
    {
        ReviewRejectedId = request.Id;
        ReviewRejectedTypeId = responseCode;
        DateOccurred = DateTime.Now;
        ErrorReason = ex;
    }
}

