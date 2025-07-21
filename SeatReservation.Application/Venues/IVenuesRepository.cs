using CSharpFunctionalExtensions;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Database;

public interface IVenuesRepository
{
    Task<Result<Guid, Error>> Add(Venue venue, CancellationToken cancellationToken = default);

    Task<Result<Guid, Error>> UpdateVenueName(VenueId venueId, VenueName venueName,
        CancellationToken cancellationToken);

    Task<UnitResult<Error>> UpdateVenueNameByPrefix(string prefix, VenueName venueName,
        CancellationToken cancellationToken);

    Task<Result<Venue, Error>> GetByIdWithSeats
        (VenueId id, CancellationToken cancellationToken);

    Task<Result<Venue, Error>> GetVenueById
        (VenueId id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Venue>> GetVenueByPrefix
        (string prefix, CancellationToken cancellationToken);

    Task<UnitResult<Error>> DeleteSeatsByVenueId
        (VenueId venueId, CancellationToken cancellationToken);

    Task<UnitResult<Error>> AddSeats(IEnumerable<Seat> seats, CancellationToken cancellationToken);

    
}