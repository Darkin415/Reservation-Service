using CSharpFunctionalExtensions;
using SeatReservation.Domain.Venue;

namespace SeatReservation.Domain.Events;

public record EventId
{
    public EventId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static Result<EventId, string> Create(Guid value)
    {
        if (value == Guid.Empty)
            return "Id cannot be empty";

        return new EventId(value);
    }
}

public class Event
{
    private Event()
    {

    }
    public Event(EventId id, VenueId venueId, EventDetails details, string name, DateTime eventDate)
    {
        Id = id;
        Details = details;
        VenueId = venueId;
        Name = name;
        EventDate = eventDate;
    }
    public EventId Id { get; set; }

    public EventDetails Details { get; set; }

    public EventType Type { get; private set; }
    public VenueId VenueId { get; set; }   
    
    public IEventInfo Info {get; private set;}

    public interface IEventInfo { }
    public string Name { get; set; }    

    public DateTime EventDate { get; set; }

    public enum EventType
    {
        Concert,
        Conference,
        Online
    }
    
    public record ConcertInfo(string Performer): IEventInfo;
    
    public record ConferenceInfo(string Speaker, string Topic):IEventInfo;
    
    public record OnlineInfo(string Url):IEventInfo;

    
}
