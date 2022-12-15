namespace Reviews.CommandApi.Core.Entities.Responses
{
    public abstract record ResponseBase 
    {
        public virtual string Type { get; } = "base-response";
        public Guid Id { get; init; }
        public DateTime ResponseDate { get; init; }
        public string Message { get; init; }
    }
}
