using MediatR;
using TariffComparison.API.Abstractions;
using TariffComparison.Application.Queries;

namespace TariffComparison.API.Endpoints
{
    public class TariffEndpoints : IMinimalEndpoint
    {
        public void MapRoutes(IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("/compare", GetComparedTariffsSorted)
              .WithName(nameof(GetComparedTariffsSorted))
              .WithOpenApi(); 
        }

        public static async Task<IResult> GetComparedTariffsSorted(IMediator mediator, int consumption)
        {
            if (consumption < 0)
            {
                return Results.BadRequest("Consumption must be at least 0");
            }

            var query = new GetAnnualCosts { ConsumptionKwhPerYear = consumption };
            var result = await mediator.Send(query);

            return Results.Ok(result);
        }
    }
}