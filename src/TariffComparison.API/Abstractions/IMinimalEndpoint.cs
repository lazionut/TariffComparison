namespace TariffComparison.API.Abstractions
{
    public interface IMinimalEndpoint
    {
        internal void MapRoutes(IEndpointRouteBuilder routeBuilder);
    }
}