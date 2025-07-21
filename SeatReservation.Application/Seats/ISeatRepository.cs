using CSharpFunctionalExtensions;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Seats;

public interface ISeatRepository
{
    Task <IReadOnlyList<Seat>> GetByIds(
        IEnumerable<SeatId> seatIds, CancellationToken cancellationToken);
}