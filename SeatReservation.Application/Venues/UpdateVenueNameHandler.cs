using CSharpFunctionalExtensions;
using SeatReservation.Application.Database;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Venues;

public class UpdateVenueNameHandler
{
    private readonly IVenuesRepository _repository;
    private readonly ITransactionManager _transactionManager;

    public UpdateVenueNameHandler(IVenuesRepository repository, ITransactionManager transactionManager)
    {
        _repository = repository;
        _transactionManager = transactionManager;
    }
    public async Task<Result<Guid, Error>> Handle(UpdateVenueNameRequest request, CancellationToken cancellationToken)
    {
        var venueId = new VenueId(request.Id);
        
        var venueResult = await _repository.GetVenueById(venueId, cancellationToken);
        if (venueResult.IsFailure)
           return venueResult.Error;

        var venue = venueResult.Value;

        venue.UpdateName(request.Name);

        await _transactionManager.SaveChangesAsync(cancellationToken);

        return venueId.Value;
    }
}