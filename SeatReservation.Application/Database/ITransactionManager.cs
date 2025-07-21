using CSharpFunctionalExtensions;
using SeatReservation.Domain;

namespace SeatReservation.Infrastructure.Postgres.Database;

public interface ITransactionManager
{
    Task<Result<ITransactionScope, Error>> BeginTransactionAsync(CancellationToken cancellationToken);

    Task<UnitResult<Error>> SaveChangesAsync(CancellationToken cancellationToken);
}