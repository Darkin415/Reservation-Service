namespace SeatReservation.Contracts.Requests;

public record UpdateVenueSeatsRequest(Guid VenueId, IEnumerable<UpdateSeatDto> Seats);