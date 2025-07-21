namespace SeatReservation.Contracts.Requests;

public record UpdateVenueNameRequest(Guid Id, string Name);