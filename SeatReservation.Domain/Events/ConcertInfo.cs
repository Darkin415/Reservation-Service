namespace SeatReservation.Domain.Events;

public class ConcertInfo : Event.IEventInfo
{
    public string Performer { get; }

    public ConcertInfo(string performer)
    {
        Performer = performer;
    }
}