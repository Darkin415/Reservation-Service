using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeatReservation.Application.Reservations;
using SeatReservation.Domain;
using SeatReservation.Domain.Reservations;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly ReservationServiceDbContext _dbContext;
    private readonly ILogger<ReservationsRepository> _logger;

    public ReservationsRepository(ReservationServiceDbContext dbContext, ILogger<ReservationsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Add(Reservation reservation, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.Reservations.AddAsync(reservation, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return reservation.Id.Value;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to add reservation ");
            return Error.Failure("reservation.add", "Fail to add reservation");
        }
        
    }

    public async Task<bool> AnySeatsAlreadyReserved(Guid eventId, IEnumerable<SeatId> seatIds,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Reservations
            .Where(r => r.EventId.Value == eventId)
            .Where(r => r.ReservedSeats.Any(rs => seatIds.Contains((rs.SeatId))))
            .AnyAsync(cancellationToken);

        
    }
}