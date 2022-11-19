using Reviews.CommandApi.Core.Interfaces.Data;
using Reviews.CommandApi.Shared.Exceptions;
using System.Data;

namespace Reviews.CommandApi.Infra.Data;
public class UnitOfWork : IUnitOfWork
{
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; protected set; }

    private int _transactionCounter = default;
    private bool _disposed;

    public UnitOfWork(IDbConnection connection) =>
        Connection = connection;

    public void BeginTransaction()
    {
        if (_transactionCounter == 0)
        {
            if (Connection.State is not ConnectionState.Open)
            {
                Connection.Open();
            }

            Transaction = Connection.BeginTransaction();
        }

        _transactionCounter++;
    }

    public void Commit()
    {
        try
        {
            if (Transaction is null || _transactionCounter < 0)
                throw new NotOpenTransactionException("Commit");

            _transactionCounter--;

            if (_transactionCounter > 0)
                return;

            Transaction.Commit();
            ClearTransaction();
        }
        catch (NotOpenTransactionException)
        {
            throw;
        }
        catch (Exception)
        {
            Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        if (Transaction is null)
            return;

        _transactionCounter = 0;

        Transaction.Rollback();
        ClearTransaction();
    }

    private void ClearTransaction()
    {
        Transaction.Dispose();
        Transaction = null;
        Connection.Close();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            Transaction?.Dispose();
            Connection?.Dispose();
        }

        _disposed = true;    
    }
}
