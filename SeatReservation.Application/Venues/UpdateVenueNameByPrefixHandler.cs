using CSharpFunctionalExtensions;
using SeatReservation.Application.Database;
using SeatReservation.Contracts.Requests;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Venues;

public class UpdateVenueNameByPrefixHandler
{
    private readonly IVenuesRepository _repository;
    public UpdateVenueNameByPrefixHandler(IVenuesRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Guid, Error>> Handle(UpdateVenueNameRequest request, CancellationToken cancellationToken)
    {
        var venueId = new VenueId(request.Id);

        var venueName = VenueName.CreateWithoutPrefix(request.Name);
        if (venueName.IsFailure)
            return venueName.Error;
        
        var result = await _repository.UpdateVenueName(venueId, venueName.Value, cancellationToken);

        if (result.IsFailure)
            return result.Error;

        return result.Value;
    }
}