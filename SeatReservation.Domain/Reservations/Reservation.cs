using CSharpFunctionalExtensions;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Domain.Reservations;

public record ReservationId(Guid Value);
public class Reservation
{
    private List<ReservationSeat> _reservedSeats;

    private Reservation()
    {
        
    }
    public Reservation(ReservationId id, EventId eventId, Guid userId, IEnumerable<SeatId> seatIds)
    {
        Id = id;
        EventId = eventId;
        UserId = userId; 
        Status = ReservationStatus.Pending;
        CreatedAt = DateTime.UtcNow;

        var reserveSeats = seatIds
            .Select(seatId => new ReservationSeat(new ReservationSeatId(Guid.NewGuid()), this, seatId))
            .ToList();

        _reservedSeats = reserveSeats;
    }
   
    public ReservationId Id { get; private set; }

    public EventId EventId {  get; private set; }

    public Guid UserId { get; private set; }    

    public ReservationStatus Status { get; private set; }   

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<ReservationSeat> ReservedSeats => _reservedSeats;

    public static Result<Reservation, Error> Create(
        EventId eventId,
        Guid userId,
        IEnumerable<Guid> seatIds)
    {
        if (eventId.Value == Guid.Empty)
        {
            return Error.Validation("reservation.eventId", "Event ID cannot be empty");
        }

        if (userId == Guid.Empty)
        {
            return Error.Validation("reservation.userId", "User ID cannot be empty");
        }

        var seatIdsList = seatIds?
            .Select(seatGuid => new SeatId(seatGuid))  
            .ToList() ?? [];

        if (seatIdsList.Count == 0)
        {
            return Error.Validation("reservation.seats", "At least one seat must be selected");
        }

        if (seatIdsList.Any(seatId => seatId.Value == Guid.Empty))
        {
            return Error.Validation("reservation.seats", "Seat IDs cannot be empty");
        }

        return new Reservation(new ReservationId(Guid.NewGuid()), eventId, userId, seatIdsList);
    }
}
