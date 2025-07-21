using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venues;

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
    
    public static Result<VenueName, Error> CreateWithoutPrefix(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("venue.name", "Venue name cannot be empty or whitespace");
        }
    
        if (name.Length > ConstantsLength.LENGTH500)
        {
            return Error.Validation("venue.name", "Venue name is too long");
        }
    
        return new VenueName("", name);
    }
}
