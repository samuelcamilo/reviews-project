namespace Reviews.CommandApi.Core.Models.Responses
{
    public abstract record BaseResponse 
    {
        public Guid Id { get; init; }
        public virtual string Type { get; } = "base-response";
        public DateTime ResponseDate { get; init; }
    }
}
