using CSharpFunctionalExtensions;
using SeatReservation.Domain;
using SeatReservation.Domain.Events;

namespace SeatReservation.Application.Events;

public interface IEventsRepository
{
    Task<Result<Event, Error>> GetById(EventId eventId, CancellationToken cancellationToken);
    
    Task<Result<Guid, Error>> Add(Event @event, CancellationToken cancellationToken);
}