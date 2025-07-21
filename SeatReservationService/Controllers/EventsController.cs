using Microsoft.AspNetCore.Mvc;
using SeatReservation.Application.Events;
using SeatReservation.Application.Reservations;
using SeatReservation.Contracts.Requests;

namespace SeatReservationService.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] CreateEventRequest request,
        [FromServices] CreateEventHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);
        
        return Ok(result.Value);
    }
}