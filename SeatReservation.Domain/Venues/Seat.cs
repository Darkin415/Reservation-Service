using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venue;

public record SeatId(Guid Value);
public class Seat
{
    private Seat()
    {
        
    }
    public Seat(SeatId id, int rowNumber, int seatNumber)
    {
        Id = id;
        RowNumber = rowNumber;
        SeatNumber = seatNumber;  
    }
    public SeatId Id { get;  set; }
    
    public VenueId VenueId { get;  set; } 

    public int RowNumber { get; set; }

    public int SeatNumber { get; set; }
 

    public static Result<Seat, Error> Create(int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0 || seatNumber <= 0)
            return Error.Validation("seat.rowNumber", "Row number and seat number must be greater than zero");

        return new Seat(new SeatId(Guid.NewGuid()), rowNumber, seatNumber);
    }
}
