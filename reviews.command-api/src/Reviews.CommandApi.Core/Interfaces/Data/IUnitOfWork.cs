using System.Data;

namespace Reviews.CommandApi.Core.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    { 
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
