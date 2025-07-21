using CSharpFunctionalExtensions;
using SeatReservation.Application.Database;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Venues;

public class UpdateVenueSeatsHandler
{
    private readonly IVenuesRepository _repository;
    private readonly ITransactionManager _transactionManager;

    public UpdateVenueSeatsHandler(IVenuesRepository repository, ITransactionManager transactionManager)
    {
        _repository = repository;
        _transactionManager = transactionManager;
    }
    public async Task<Result<Guid, Error>> Handle(UpdateVenueSeatsRequest request, CancellationToken cancellationToken)
    {
        var venueId = new VenueId(request.VenueId);

        var transactionScopeResult = await _transactionManager.BeginTransactionAsync(cancellationToken);
        if (transactionScopeResult.IsFailure)
        {
            return transactionScopeResult.Error;
        }

        using var transactionScope = transactionScopeResult.Value;
        
        var venueResult = await _repository.GetVenueById(venueId, cancellationToken);
        if (venueResult.IsFailure)
        {
            transactionScope.RollBack();
            return venueResult.Error;
        }
            

        var venue = venueResult.Value;
        
        List<Seat> seats = [];

        foreach (var seatRequest in request.Seats)
        {
            var seat = Seat.Create(venueId, seatRequest.RowNumber, seatRequest.SeatNumber);

            if (seat.IsFailure)
            {
                transactionScope.RollBack();
                
                return seat.Error;
            }
                

            seats.Add(seat.Value);
        }
        
        venue.UpdateSeats(seats);
         
        await _repository.DeleteSeatsByVenueId(venueId, cancellationToken);

        await _transactionManager.SaveChangesAsync(cancellationToken);

        var commitedResult = transactionScope.Commit();
        if (commitedResult.IsFailure)
            return commitedResult.Error;

        return venueId.Value;
    }
}