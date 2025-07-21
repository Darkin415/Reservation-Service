namespace SeatReservation.Domain.Events;

public class ConcertInfo : Event.IEventInfo
{
    public string Performer { get; }

    public ConcertInfo(string performer)
    {
        Performer = performer;
    }
}

public record OnlineInfo(string Url) : Event.IEventInfo
{
    public override string ToString() => $"Online:{Url}";
}