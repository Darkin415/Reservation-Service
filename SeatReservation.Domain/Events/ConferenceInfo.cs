namespace SeatReservation.Domain.Events;

public record ConferenceInfo(string Speaker, string Topic) : Event.IEventInfo
{
    public override string ToString() => $"Conference:{Speaker}|{Topic}";
}