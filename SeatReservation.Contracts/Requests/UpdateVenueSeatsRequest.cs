namespace SeatReservation.Application.Venues;

public record UpdateVenueSeatsRequest(Guid VenueId, IEnumerable<UpdateSeatRequest> Seats);