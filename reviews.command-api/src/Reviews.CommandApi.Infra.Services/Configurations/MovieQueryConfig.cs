namespace Reviews.CommandApi.Infra.Services.Configurations
{
    public class MovieQueryConfig
    {
        public string BaseUrl { get; set; }
        public string ResourceRoute { get; set; }
        public uint TimeoutInMilliseconds { get; set; }
        public string AuthorizationToken { get; set; }
    }
}
