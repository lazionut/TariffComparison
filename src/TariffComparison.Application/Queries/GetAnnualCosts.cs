using MediatR;
using TariffComparison.Domain.Models;

namespace TariffComparison.Application.Queries
{
    public class GetAnnualCosts : IRequest<List<CalculationResult>>
    {
        public double ConsumptionKwhPerYear { get; set; }
    }
}