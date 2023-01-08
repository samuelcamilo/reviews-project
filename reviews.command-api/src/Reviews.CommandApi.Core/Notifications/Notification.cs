using Reviews.CommandApi.Core.Interfaces.Notifications;
using System.Text;

namespace Reviews.CommandApi.Core.Notifications;

internal class Notification : INotification
{
    private readonly Queue<(string message, int statusCode)> _messages = new();

    public void Add(string message, int statusCode = -1) =>
        _messages.Enqueue((message, statusCode));

    public void Add((string message, int statusCode) message) =>
        Add(message.message, message.statusCode);

    public IEnumerable<(string message, int statusCode)> All() =>
        _messages.AsEnumerable();

    public bool Any() =>
        _messages.Count > 0;

    public int GetStatusCode() =>
        _messages.Peek().statusCode;

    public string GetSummary() =>
        _messages.Aggregate(
            new StringBuilder(),
            (sb, message) => sb.AppendLine(message.message))
        .ToString();
}

