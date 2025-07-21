using CSharpFunctionalExtensions;
using SeatReservation.Domain;

namespace SeatReservation.Application.Database;

public interface ITransactionScope : IDisposable
{
    UnitResult<Error> Commit();
    
    UnitResult<Error> RollBack();


}