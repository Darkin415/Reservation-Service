using CSharpFunctionalExtensions;
using SeatReservation.Domain;
using SeatReservation.Domain.Reservations;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public interface IReservationsRepository
{
    Task<Result<Guid, Error>> Add(Reservation reservation, CancellationToken cancellationToken);

}