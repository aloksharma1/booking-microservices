using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Web;
using Flight.Aircrafts.Dtos;
using Flight.Aircrafts.Features.CreateAircraft.Commands.V1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;

public class CreateAircraftEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost($"{EndpointConfig.BaseApiPath}/flight/aircraft", CreateAircraft)
            .RequireAuthorization()
            .WithTags("Flight")
            .WithName("Create Aircraft")
            .WithMetadata(new SwaggerOperationAttribute("Create Aircraft", "Create Aircraft"))
            .WithApiVersionSet(endpoints.NewApiVersionSet("Flight").Build())
            .Produces<AircraftResponseDto>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .HasApiVersion(1.0);

        return endpoints;
    }

    private async Task<IResult> CreateAircraft(CreateAircraftCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}
