using SeatReservation.Domain.Venue;

namespace SeatReservation.Application;

public class CreateVenueHandler
{
    public async Task Handle(CreateVenueRequest request, CancellationToken cancellationToken)
    {
        var seats = request.Seats.Select(s => Seat.Create(s.RowNumber, s.SeatNumber).Value);
        var venue = Venue.Create(request.Prefix, request.Name, request.SeatsLimit, seats);
    }
}