using Reviews.CommandApi.Core.Entities;

namespace Reviews.CommandApi.Infra.Data.Repositories.Statements
{
    public static class ReviewStmt
    {
        public static readonly string InsertReview = $@"
            INSERT INTO REVIEW (
                REVIEW_UUID, 
                MOVIE_UUID, 
                TITLE_NM, 
                MESSAGE_TX, 
                CREATED_AT_DT, 
                CREATED_BY_DS)
            VALUES (
                @{nameof(ReviewEntity.Id)},
                @{nameof(ReviewEntity.MovieId)},
                @{nameof(ReviewEntity.Title)},
                @{nameof(ReviewEntity.Message)},
                @{nameof(ReviewEntity.CreatedAt)},
                @{nameof(ReviewEntity.CreatedBy)}
            );";
    }
}
