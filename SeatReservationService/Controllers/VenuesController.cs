using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using SeatReservation.Application;
using SeatReservation.Application.Venues;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;

namespace SeatReservationService.Controllers;
[ApiController]
[Route("api/venues")]
public class VenuesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVenueHandler handler,
        [FromBody] CreateVenueRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPatch("/name")]

    public async Task<IActionResult> UpdateVenueName(
        [FromServices] UpdateVenueNameHandler handler,
        [FromBody] UpdateVenueNameRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);
        
        return Ok(result.Value);
    }
    
    [HttpPatch("/by-prefix")]
    
    public async Task<IActionResult> UpdateVenueNameByPrefix(
        [FromServices] UpdateVenueNameByPrefixHandler handler,
        [FromBody] UpdateVenueNameByPrefixRequest request,
        CancellationToken cancellationToken)
    {

        var result = await handler.Handle(request, cancellationToken);

        if (result.IsSuccess)
            return Ok(); 
        else
            return BadRequest(result.Error);
    }
    
    [HttpPatch("/seats")]
    
    public async Task<IActionResult> UpdateSeats(
        [FromServices] UpdateVenueSeatsHandler handler,
        [FromBody] UpdateVenueSeatsRequest request,
        CancellationToken cancellationToken)
    {

        var result = await handler.Handle(request, cancellationToken);

        if (result.IsSuccess)
            return Ok(); 
        else
            return BadRequest(result.Error);
    }
    
    
}