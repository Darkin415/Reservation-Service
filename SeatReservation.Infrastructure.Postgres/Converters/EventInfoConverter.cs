using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Converters;

public class EventInfoConverter : ValueConverter<Event.IEventInfo, string>
{
    public EventInfoConverter() : base(i => InfoToString(i), s => StringToInfo(s))
    {
            
    }
        
    private static string InfoToString(Event.IEventInfo info) => info switch
    {
        Event.ConcertInfo c => $"Concert: {c.Performer}",
        Event.ConferenceInfo c => $"Conference: {c.Speaker}| {c.Topic}",
        Event.OnlineInfo o => $"Online: {o.Url}",
        _ => throw new NotSupportedException("Unsupported event info type")
    };

    private static Event.IEventInfo StringToInfo(string info)
    {
        var split = info.Split(':', 2);
        var type = split[0];
        var data = split[1];
        return type switch
        {
            "Concert" => new Event.ConcertInfo(data),
            "Conference" => new Event.ConferenceInfo(data.Split("|")[0], data.Split("|")[1]),
            "Online" => new Event.OnlineInfo(data),
            _ => throw new NotSupportedException($"Unknown type {type}")
        };
    }
}