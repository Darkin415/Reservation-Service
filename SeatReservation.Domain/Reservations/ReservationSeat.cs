using SeatReservation.Domain.Venues;

namespace SeatReservation.Domain.Reservations;

public record ReservationSeatId(Guid Value);
public class ReservationSeat
{
    private ReservationSeat()
    {
        
    }
    public ReservationSeat(ReservationSeatId id, Reservation reservation, SeatId seatId)
    {
        Id = id;
        Reservation = reservation;
        SeatId = seatId;
        ReserveAt = DateTime.UtcNow;
    }
    public ReservationSeatId Id { get;  set; }    

    public Reservation Reservation { get; private  set; }     

    public SeatId SeatId { get; private set; }    

    public DateTime ReserveAt { get; private set; }

}
