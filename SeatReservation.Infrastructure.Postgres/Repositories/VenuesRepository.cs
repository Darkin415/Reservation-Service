using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatReservation.Application.Database;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public class EfCoreVenuesRepository : IVenuesRepository
{
    private readonly ReservationServiceDbContext _dbContext;
    public EfCoreVenuesRepository(ReservationServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Add(Venue venue, CancellationToken cancellationToken)
    {
        await _dbContext.Venues.AddAsync(venue, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return venue.Id.Value;
    }

    public async Task<Result<Guid, Error>> UpdateVenueName(VenueId venueId, VenueName venueName, CancellationToken cancellationToken)
    {
        await _dbContext
            .Venues
            .Where(v => v.Id == venueId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(v => v.VenueName.Name, venueName.Name), cancellationToken);

        return venueId.Value;
    }
    
    public async Task<UnitResult<Error>> UpdateVenueNameByPrefix
        (string prefix, VenueName venueName, CancellationToken cancellationToken)
    {
        await _dbContext
            .Venues
            .Where(v => v.VenueName.Prefix.StartsWith(prefix))
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(v => v.VenueName.Name, venueName.Name), cancellationToken);

        return UnitResult.Success<Error>();
    }

    public async Task<UnitResult<Error>> AddSeats(IEnumerable<Seat> seats, CancellationToken cancellationToken)
    {
        await _dbContext.Seats.AddRangeAsync(seats, cancellationToken);

        return UnitResult.Success<Error>();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Result<Venue, Error>> GetVenueById
        (VenueId id, CancellationToken cancellationToken)
    {
        var venue = await _dbContext.Venues 
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        if (venue is null)
            return Error.NotFound("venue.not.found", "Venue not found");

        return venue;
        
    }
    
    public async Task<Result<Venue, Error>> GetByIdWithSeats
        (VenueId id, CancellationToken cancellationToken)
    {
        var venue = await _dbContext.Venues 
            .Include(v => v.Seats)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        if (venue is null)
            return Error.NotFound("venue.not.found", "Venue not found");

        return venue;
        
    }
    
    public async Task<IReadOnlyList<Venue>> GetVenueByPrefix
        (string prefix, CancellationToken cancellationToken)
    {
        var venue = await _dbContext.Venues
            .Where(v => v.VenueName.Prefix.StartsWith(prefix))
            .ToListAsync(cancellationToken);
        

        return venue;
        
    }
    
    
    
    public async Task<UnitResult<Error>> DeleteSeatsByVenueId
        (VenueId venueId, CancellationToken cancellationToken)
    {
        await _dbContext.Seats.Where(s => s.Venue.Id == venueId)
            .ExecuteDeleteAsync(cancellationToken);
            

        return UnitResult.Success<Error>();
    }
}