using System.Net;

namespace Reviews.CommandApi.Core.Constants;

public static class ResponseCodes
{
    public const int InternalServerError = (int)HttpStatusCode.InternalServerError;
    public const int UnprocessableEntity = (int)HttpStatusCode.UnprocessableEntity;
    public const int NonExistentMovie = (int)HttpStatusCode.NotFound;

    public static string GetDescriptionTo(int statusCode) => statusCode switch
    {
        InternalServerError => "Internal server error.",
        UnprocessableEntity => "Unprocessable entity.",
        NonExistentMovie => "Movie non exists.",
        _ => statusCode.ToString(),
    };
}
