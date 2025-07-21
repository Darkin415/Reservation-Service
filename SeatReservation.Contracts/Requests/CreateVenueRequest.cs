namespace SeatReservation.Contracts.Requests;

public record CreateVenueRequest(string Name, string Prefix, int SeatsLimit, IEnumerable<CreateSeatRequest> Seats);