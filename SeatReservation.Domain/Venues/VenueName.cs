using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venue;

public record VenueName
{
    public VenueName(string prefix, string name)
    {
        Prefix = prefix;
        Name = name;
    }
    public string Prefix { get; }

    public string Name { get; }

    public override string ToString() => $"{Prefix}-{Name}";
    

    public static Result<VenueName, Error> Create(string prefix, string name)
    {

        if (string.IsNullOrWhiteSpace(prefix) || string.IsNullOrWhiteSpace(name))
            return Error.Validation("venue.name", "Venue name can not be empty or whitespace");

        if (prefix.Length > ConstantsLength.LENGTH50 || name.Length > ConstantsLength.LENGTH50)
            return Error.Validation("venue.name", "Venue name is too long");

        return new VenueName(prefix, name);
    }
}
