namespace SeatReservation.Domain.Events;

public record EventDetailsId(Guid Value);
public class EventDetails
{
    private EventDetails()
    {
        
    }
    public EventDetails(EventDetailsId eventDetailsId, EventId eventId, int capacity, string description)
    {
        Id = eventDetailsId;
        EventId = eventId;
        Capacity = capacity;
        Description = description;
    }
    
    public EventDetailsId Id { get; set; }

    public EventId EventId { get;  set; } 

    public int Capacity { get;  set; }

    public string Description { get;  set; } 


}
