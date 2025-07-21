using CSharpFunctionalExtensions;
using SeatReservation.Application.Database;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Venues;

public class CreateVenueHandler
{
    private readonly IVenuesRepository _venuesRepository;
    public CreateVenueHandler(IVenuesRepository venuesRepository)
    {
        _venuesRepository = venuesRepository;
    }
    public async Task<Result<Guid,Error>> Handle(CreateVenueRequest request, CancellationToken cancellationToken)
    {

        var venueResult = Venue.Create(request.Prefix, request.Name, request.SeatsLimit);
        if (venueResult.IsFailure)
            return venueResult.Error;

        var venue = venueResult.Value;
        
        List<Seat> seats = [];

        foreach (var seatRequest in request.Seats)
        {
            var seat = Seat.Create(venue, seatRequest.RowNumber, seatRequest.SeatNumber);
            if (seat.IsFailure)
                return seat.Error;

            seats.Add(seat.Value);
            
            venue.AddSeat(seat.Value);
        }
        
        
        await _venuesRepository.Add(venue, cancellationToken);

        return venue.Id.Value;


    }
}