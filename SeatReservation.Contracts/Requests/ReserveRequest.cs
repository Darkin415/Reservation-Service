namespace SeatReservation.Contracts.Requests;

public record ReserveRequest(Guid EventId, Guid UserId, IEnumerable<Guid> SeatIds);