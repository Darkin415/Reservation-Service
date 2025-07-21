using CSharpFunctionalExtensions;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Domain.Events;

public class Event
{
    public EventId Id { get; }
    public VenueId VenueId { get; }
    public string Name { get; }
    public DateTime EventDate { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public EventDetails Details { get; }
    public EventType Type { get; }
    public EventStatus Status { get; private set; }
    public IEventInfo Info { get; }

    private Event(IEventInfo info)
    {
        Info = info;
    }

    public Event(
        EventId id,
        VenueId venueId,
        EventDetails? details,
        string name,
        DateTime eventDate,
        DateTime startDate,
        DateTime endDate,
        EventType type)
    {
        Id = id;
        VenueId = venueId;
        Name = name;
        EventDate = eventDate;
        StartDate = startDate;
        EndDate = endDate;
        Details = details;
        Type = type;
        Status = EventStatus.Planned;
        
    }

    public interface IEventInfo { }

    public enum EventType
    {
        Concert,
        Conference,
        Online
    }

    public bool IsAvaribleForReservation() => Status == EventStatus.Planned && StartDate > DateTime.UtcNow;

    public static Result<EventDetails, Error> Validate(
        EventDetailsId id,
        string name,
        DateTime eventDate,
        DateTime startDate,
        DateTime endDate,
        int capacity,
        string description)
    {
        if (startDate >= endDate || startDate <= DateTime.UtcNow || endDate <= DateTime.UtcNow)
        {
            return Error.Validation("event.time", "The time of the event is indicated incorrectly");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("event.name", "Event name cannot be empty");
        }

        if (eventDate < DateTime.UtcNow)
        {
            return Error.Validation("event.date", "Event date cannot be in the past");
        }

        if (capacity <= 0)
        {
            return Error.Validation("event.capacity", "Capacity must be greater than zero");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Error.Validation("event.description", "Description cannot be empty");
        }

        return new EventDetails(id, capacity, description);
    }
    
    public static Result<Event, Error> Create(
        VenueId venueId,
        EventDetails eventDetails,
        string name,
        DateTime eventDate,  
        DateTime startDate,  
        DateTime endDate,
        EventType type)
    {

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Event, Error>(Error.Validation("name.error", "Name can not be empty"));
        
        
        return new Event(
            new EventId(Guid.NewGuid()),
            venueId, 
            eventDetails,
            name, 
            eventDate,
            startDate, 
            endDate,
            type);
    }
}

public static class EventFactory
{
    public static Result<Event, Error> CreateConcert(
        EventDetailsId eventDetailsId,
        VenueId venueId,
        string name,
        DateTime eventDate,
        DateTime startDate,
        DateTime endDate,
        int capacity,
        string description,
        string performer)
    {
        var detailsResult = Event.Validate(eventDetailsId, name, eventDate, startDate, endDate, capacity, description);
        if (detailsResult.IsFailure)
        {
            return detailsResult.Error;
        }

        if (string.IsNullOrWhiteSpace(performer))
        {
            return Error.Validation("event.performer", "Performer cannot be empty");
        }

        var concertInfo = new ConcertInfo(performer);

        return new Event(
            new EventId(Guid.NewGuid()),
            venueId,
            detailsResult.Value,
            name,
            eventDate,
            startDate,
            endDate,
            Event.EventType.Concert);
    }
}

public enum EventStatus
{
    Planned,
    InProgress,
    Finished,
    Cancelled
}


