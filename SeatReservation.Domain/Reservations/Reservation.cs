using CSharpFunctionalExtensions;
using SeatReservation.Domain.Events;

namespace SeatReservation.Domain.Reservation;

public record ReservationId(Guid Value);
public class Reservation
{
    private List<ReservationSeat> _seats;

    private Reservation()
    {
        
    }
    public Reservation(ReservationId id, EventId eventId, Guid userId, IEnumerable<Guid> seatIds)
    {
        Id = id;
        EventId = eventId;
        UserId = userId; 
        Status = ReservationStatus.Pending;
        CreatedAt = DateTime.UtcNow;

        var reserveSeats = seatIds
            .Select(seatId => new ReservationSeat(new ReservationSeatId(Guid.NewGuid()), this, seatId))
            .ToList();

        _seats = reserveSeats;
    }
   
    public ReservationId Id { get; private set; }

    public EventId EventId {  get; private set; }

    public Guid UserId { get; private set; }    

    public ReservationStatus Status { get; private set; }   

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<ReservationSeat> Seats => _seats;

    
}
