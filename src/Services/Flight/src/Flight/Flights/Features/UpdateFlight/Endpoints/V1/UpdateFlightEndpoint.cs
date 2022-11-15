using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Web;
using Flight.Flights.Dtos;
using Flight.Flights.Features.UpdateFlight.Commands.V1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

public class UpdateFlightEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut($"{EndpointConfig.BaseApiPath}/flight", UpdateFlight)
            .RequireAuthorization()
            .WithTags("Flight")
            .WithName("Update Flight")
            .WithMetadata(new SwaggerOperationAttribute("Update Flight", "Update Flight"))
            .WithApiVersionSet(endpoints.NewApiVersionSet("Flight").Build())
            .Produces<FlightResponseDto>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .HasApiVersion(1.0);

        return endpoints;
    }

    private async Task<IResult> UpdateFlight(UpdateFlightCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}
