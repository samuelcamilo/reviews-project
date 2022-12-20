namespace Reviews.CommandApi.Core.Interfaces.Notifications;

public interface INotification
{
    void Add(string message, int statusCode = -1);

    void Add((string message, int statusCode) message);

    bool Any();

    IEnumerable<(string message, int statusCode)> All();

    int GetStatusCode();

    string GetSummary();
}

