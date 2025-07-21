using System.Data;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SeatReservation.Application.Database;
using SeatReservation.Domain;

namespace SeatReservation.Infrastructure.Postgres.Database;

public class TransactionScope : ITransactionScope
{
    private readonly IDbTransaction _transaction;
    private readonly ILogger<TransactionScope> _logger;

    public TransactionScope(IDbTransaction transaction, ILogger<TransactionScope> logger)
    {
        _transaction = transaction;
        _logger = logger;
    }

    public UnitResult<Error> Commit()
    {
        try
        {
            _transaction.Commit();
            return UnitResult.Success<Error>();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to commit transaction");
            return UnitResult.Failure(Error.Failure("transaction.commit.failed", "Failed to commit transaction"));
        }
    }
    
    public UnitResult<Error> RollBack()
    {
        try
        {
            _transaction.Rollback();
            return UnitResult.Success<Error>();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to rollback transaction");
            return UnitResult.Failure(Error.Failure("transaction.rollback.failed", "Failed to commit transaction"));
        }
    }

    public void Dispose()
    {
        _transaction.Dispose();
    }
}