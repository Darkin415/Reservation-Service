using CSharpFunctionalExtensions;

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