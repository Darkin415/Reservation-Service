namespace SeatReservationService.Controllers;

public record ReserveRequest(Guid EventId, Guid UserId, IEnumerable<Guid> SeatIds);