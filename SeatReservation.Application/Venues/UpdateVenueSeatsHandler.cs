using CSharpFunctionalExtensions;
using SeatReservation.Application.Database;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Venues;

public class UpdateVenueSeatsHandler
{
    private readonly IVenuesRepository _repository;
    public UpdateSeatsHandler(IVenuesRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Guid, Error>> Handle(UpdateVenueSeatsRequest request, CancellationToken cancellationToken)
    {
        var venueId = new VenueId(request.Id);
        
        var venueResult = await _repository.GetVenueById(venueId, cancellationToken);
        if (venueResult.IsFailure)
            return venueResult.Error;

        var venue = venueResult.Value;

        venue.UpdateName(request.Name);

        await _repository.Save();

        return venueId.Value;
    }
}