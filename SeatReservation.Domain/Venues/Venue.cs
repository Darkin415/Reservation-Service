using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venues;

public record VenueId(Guid Value);
public class Venue
{
    private Venue()
    {
        
    }
    private List<Seat> _seats = [];

    
    public Venue(VenueId id, VenueName name,  int seatsLimit)
    {
        Id = id;
        SeatsLimit = seatsLimit;
        VenueName = name;
    }
    public VenueId Id { get;  set;}

    public int SeatsLimit { get;  set;}   

    public int SeatsCount => _seats.Count;

    public VenueName VenueName { get; private set; } = null!;

    public IReadOnlyList<Seat> Seats => _seats;

    public UnitResult<Error> AddSeat(Seat seat)
    {
        if (SeatsCount > SeatsLimit)
            return Error.Conflict("venue.seats.limit", "");

        _seats.Add(seat);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> UpdateSeats(IEnumerable<Seat> seats)
    {
        var seatsList = seats.ToList();
        if (seatsList.Count() > SeatsLimit)
            return Error.Failure("venue.seats.limit", "There are too many seats");
        
        _seats = seatsList.ToList();

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> UpdateName(string name)
    {
        var newVenueName = VenueName.Create(VenueName.Prefix, name);
        if (newVenueName.IsFailure)
            return newVenueName.Error;
        
        VenueName = newVenueName.Value;

        return UnitResult.Success<Error>();
    }

    public static Result<Venue, Error> Create(
        string prefix, 
        string name, 
        int seatsLimit)
    {
        if(seatsLimit< 0)
            return Error.Validation("seatsLimit", "Seats limit cannot be greater than than zero");

        var venueNameResult = VenueName.Create(prefix, name);
        if(venueNameResult.IsFailure)
            return venueNameResult.Error;
        
        
        return new Venue(new VenueId(Guid.NewGuid()), venueNameResult.Value, seatsLimit);
    }
    
   

    public void ExpandSeatsLimit(int newSeatsLimit) => SeatsLimit = newSeatsLimit;
}
