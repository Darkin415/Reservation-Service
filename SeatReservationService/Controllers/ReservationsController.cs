using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Application.Reservations;
using SeatReservation.Contracts.Requests;


namespace SeatReservationService.Controllers;
[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Reserve(
        [FromBody] ReserveRequest request,
        [FromServices] ReserveHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);
        
        return Ok(result.Value);
    }
}