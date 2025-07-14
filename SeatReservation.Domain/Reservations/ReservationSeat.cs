namespace SeatReservation.Domain.Reservation;

public record ReservationSeatId(Guid Value);
public class ReservationSeat
{
    private ReservationSeat()
    {
        
    }
    public ReservationSeat(ReservationSeatId id, Reservation reservation, Guid seatId)
    {
        Id = id;
        Reservation = reservation;
        SeatId = seatId;
        ReserveAt = DateTime.UtcNow;
    }
    public ReservationSeatId Id { get;  set; }    

    public Reservation Reservation { get; private  set; }     

    public Guid SeatId { get; private set; }    

    public DateTime ReserveAt { get; private set; }

}
