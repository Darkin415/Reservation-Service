using CSharpFunctionalExtensions;
using SeatReservation.Domain.Reservations;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Domain.Events;

public record EventDetailsId(Guid Value);
public class EventDetails
{
    private EventDetails()
    {
        
    }
    public EventDetails(EventDetailsId id, int capacity, string description)
    {
        Id = id;
        Capacity = capacity;
        Description = description;
    }
    
    public EventDetailsId Id { get; set; }

    public EventId EventId { get;  set; } 

    public int Capacity { get;  set; }

    public string Description { get;  set; } 
     
    
    public static Result<EventDetails, Error> Create(
        EventDetailsId id, int capacity, string description)
    {
        
        return new EventDetails(id, capacity, description);
    }
}
    


