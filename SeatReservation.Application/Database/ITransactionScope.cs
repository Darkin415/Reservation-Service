using CSharpFunctionalExtensions;
using SeatReservation.Domain;

namespace SeatReservation.Infrastructure.Postgres.Database;

public interface ITransactionScope : IDisposable
{
    UnitResult<Error> Commit();
    
    UnitResult<Error> RollBack();


}