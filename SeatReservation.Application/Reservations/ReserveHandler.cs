using CSharpFunctionalExtensions;
using SeatReservation.Application.Events;
using SeatReservation.Application.Seats;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Reservations;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Reservations;

public class ReserveHandler
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IEventsRepository _eventsRepository;
    private readonly ISeatRepository _seatsRepository;

    public ReserveHandler(
        IReservationsRepository reservationsRepository, 
        IEventsRepository eventsRepository,
        ISeatRepository seatsRepository)
    {
        _reservationsRepository = reservationsRepository;
        _eventsRepository = eventsRepository;
        _seatsRepository = seatsRepository;
    }
    public async Task<Result<Guid, Error>> Handle(ReserveRequest request, CancellationToken cancellationToken)
    {
        
        var eventIdResult = EventId.Create(request.EventId);

        var (_, isFailure, @event, error) = await _eventsRepository.GetById(eventIdResult.Value, cancellationToken);
        if (isFailure)
        {
            return error;
        }

        if (@event.IsAvaribleForReservation() == false)
        {
            return Error.Failure("reservation.unavailable", "Reservation is unavailable");
        }

        var seatIds = request.SeatIds.Select(id => new SeatId(id)).ToList();

        var seats = await _seatsRepository.GetByIds(seatIds, cancellationToken);

        if (seats.Any(seat => seat.VenueId != @event.VenueId) || seats.Count == 0)
        {
            return Error.Conflict("seat.conflict", "Seat does not belong to venue");
        }

        var isSeatsReserved = await _reservationsRepository
            .AnySeatsAlreadyReserved(request.EventId, seatIds, cancellationToken);
        if (isSeatsReserved)
        {
            return Error.Conflict("seats.conflict", "Seats already reserved");
        }
        var reservationResult = Reservation.Create(eventIdResult.Value, request.UserId, request.SeatIds);
        if (reservationResult.IsFailure)
        {
            return reservationResult.Error; 
        }

        await _reservationsRepository.Add(reservationResult.Value, cancellationToken);

        return reservationResult.Value.Id.Value;

    }
}