namespace SeatReservation.Application;

public record CreateVenueRequest(string Name, int SeatsLimit, IEnumerable<CreateSeatRequest> Seats);