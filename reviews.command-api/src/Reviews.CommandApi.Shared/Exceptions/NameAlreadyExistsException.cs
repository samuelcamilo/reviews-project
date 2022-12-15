namespace Reviews.CommandApi.Shared.Exceptions
{
    public class NameAlreadyExistsException : Exception
    {
        public NameAlreadyExistsException(string movieName)
            : base($"Movie {movieName} already exists.") { }
    }
}
