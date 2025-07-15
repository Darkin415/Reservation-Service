using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venue;

public record VenueId(Guid Value);
public class Venue
{
    private Venue()
    {
        
    }
    private List<Seat> _seats = [];

    
    public Venue(VenueId id, VenueName name, IEnumerable<Seat> seats, int seatsLimit)
    {
        Id = id;
        SeatsLimit = seatsLimit;
        VenueName = name;
        _seats = seats.ToList();
    }
    public VenueId Id { get;  set;}

    public int SeatsLimit { get;  set;}   

    public int SeatsCount => _seats.Count;   

    public VenueName VenueName { get;  set;}

    public IReadOnlyList<Seat> Seats => _seats;

    public UnitResult<Error> AddSeat(Seat seat)
    {
        if (SeatsCount > SeatsLimit)
            return Error.Conflict("venue.seats.limit", "");

        _seats.Add(seat);

        return UnitResult.Success<Error>();
    }

    public static Result<Venue, Error> Create(
        string prefix, 
        string name, 
        int seatsLimit, 
        IEnumerable<Seat> seats)
    {
        if(seatsLimit< 0)
            return Error.Validation("seatsLimit", "Seats limit cannot be greater than than zero");

        var venueNameResult = Domain.Venue.VenueName.Create(prefix, name);
        if(venueNameResult.IsFailure)
            return venueNameResult.Error;

        var venueSeats = seats.ToList();
        
        if(venueSeats.Count < 1)
            return Error.Validation("venue.seats.limit", "Number of seats can not be zero");
        
        return new Venue(new VenueId(Guid.NewGuid()), venueNameResult.Value, venueSeats, seatsLimit);
    }

    public void ExpandSeatsLimit(int newSeatsLimit) => SeatsLimit = newSeatsLimit;
}
