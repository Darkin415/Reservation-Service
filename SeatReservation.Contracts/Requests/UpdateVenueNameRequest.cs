namespace SeatReservation.Application.Venues;

public record UpdateVenueNameRequest(Guid Id, string Name);