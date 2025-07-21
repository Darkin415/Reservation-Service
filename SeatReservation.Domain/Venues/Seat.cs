using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venues;

public record SeatId(Guid Value);
public class Seat
{
    private Seat()
    {
        
    }
    public Seat(SeatId id, Venue venue, int rowNumber, int seatNumber)
    {
        Id = id;
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
        Venue = venue;
    }
    
    public Seat(SeatId id, VenueId venueId, int rowNumber, int seatNumber)
    {
        Id = id;
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
        VenueId = venueId;
    }
    public SeatId Id { get;  set; }
    
    public Venue Venue { get;  set; }

    public VenueId VenueId { get; private set; } = null!;

    public int RowNumber { get; set; }

    public int SeatNumber { get; set; }
    
    public static Result<Seat, Error> Create(VenueId venueId, int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0 || seatNumber <= 0)
            return Error.Validation("seat.rowNumber", "Row number and seat number must be greater than zero");

        return new Seat(new SeatId(Guid.NewGuid()), venueId, rowNumber, seatNumber);
    }
 

    public static Result<Seat, Error> Create(Venue venue, int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0 || seatNumber <= 0)
            return Error.Validation("seat.rowNumber", "Row number and seat number must be greater than zero");

        return new Seat(new SeatId(Guid.NewGuid()), venue, rowNumber, seatNumber);
    }
}
