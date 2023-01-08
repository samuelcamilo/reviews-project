using Reviews.CommandApi.Core.Entities;

namespace Reviews.CommandApi.Core.Interfaces.Data
{
    public interface IReviewRepository
    {
        Task Insert(ReviewEntity review);
    }
}
