using CSharpFunctionalExtensions;
using SeatReservation.Domain;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public interface IEventsRepository
{
    Task<Result<Event, Error>> GetById(EventId eventId, CancellationToken cancellationToken);
}