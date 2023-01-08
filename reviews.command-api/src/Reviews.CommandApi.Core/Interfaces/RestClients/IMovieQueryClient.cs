namespace Reviews.CommandApi.Core.Interfaces.RestClients
{
    public interface IMovieQueryClient
    {
        Task<bool> GetAsync(Guid movieId, CancellationToken cancellationToken);
    }
}
