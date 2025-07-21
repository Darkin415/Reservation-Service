using System.Reflection;
using CSharpFunctionalExtensions;
using SeatReservation.Application.Venues;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Events;

public class CreateEventHandler
{
    private readonly IEventsRepository _eventsRepository;
    private readonly IVenuesRepository _venuesRepository;

    public CreateEventHandler(IEventsRepository eventsRepository, IVenuesRepository venuesRepository)
    {
        _eventsRepository = eventsRepository;
        _venuesRepository = venuesRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var venueId = new VenueId(request.VenueId);

        var venueResult = await _venuesRepository.GetVenueById(venueId, cancellationToken);
            
        var eventId = EventId.Create(Guid.NewGuid());

        var eventDetailsId = new EventDetailsId(Guid.NewGuid());

        var eventDetails = EventDetails.Create(eventDetailsId, request.Capacity, request.Description);

        var eventTypeResult = Enum.TryParse(request.Type, out Event.EventType eventType);
        
        
        
        

        var @event = Event.Create(
            venueId, 
            eventDetails.Value, 
            request.Name, 
            request.EventDate, 
            request.StartDate,
            request.EndDate,
            eventType);
            
    

        await _eventsRepository.Add(@event.Value, cancellationToken);

        return @event.Value.Id.Value;
    }
}

public record CreateEventRequest (
    Guid VenueId,
    string Name,
    int Capacity,
    string Description,
    DateTime EventDate,
    DateTime StartDate,
    DateTime EndDate,
    string Type,
    string? Details,
    string? EventInfo);