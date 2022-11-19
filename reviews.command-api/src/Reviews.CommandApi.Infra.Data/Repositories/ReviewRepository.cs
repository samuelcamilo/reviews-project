using Dapper;
using Reviews.CommandApi.Core.Entities;
using Reviews.CommandApi.Core.Interfaces.Data;
using Reviews.CommandApi.Infra.Data.Repositories.Statements;

namespace Reviews.CommandApi.Infra.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IUnitOfWork _uow;

        public ReviewRepository(IUnitOfWork uow) =>
            _uow = uow;

        public async Task Insert(ReviewEntity review)
        {
            var test = ReviewStmt.InsertReview;
            await _uow.Connection
                .ExecuteAsync(
                    sql: ReviewStmt.InsertReview,
                    param: StatementsProvider.GetInsertParameters(review),
                    transaction: _uow.Transaction);
        }
    }
}
