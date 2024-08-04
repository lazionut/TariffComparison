using MediatR;
using TariffComparison.Application.Interfaces;
using TariffComparison.Application.Queries;
using TariffComparison.Domain.Helpers;
using TariffComparison.Domain.Models;

namespace TariffComparison.Application.QueryHandlers
{
    public class GetAnnualCostsHandler : IRequestHandler<GetAnnualCosts, List<CalculationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TariffCalculatorGenerator _tariffCalculatorGenerator;

        public GetAnnualCostsHandler(IUnitOfWork unitOfWork, TariffCalculatorGenerator tariffCalculatorGenerator)
        {
            _unitOfWork = unitOfWork;
            _tariffCalculatorGenerator = tariffCalculatorGenerator;
        }

        public async Task<List<CalculationResult>> Handle(GetAnnualCosts request, CancellationToken cancellationToken)
        {
            var electricityTariffs = await _unitOfWork.TariffRepository.GetAll();

            double annualCost = 0;

            var calculatedTariffs = new List<CalculationResult>();

            foreach (var electricityTariff in electricityTariffs)
            {
                var calculator = _tariffCalculatorGenerator.Generate(electricityTariff);
                annualCost = calculator.CalculateAnnualCosts(request.ConsumptionKwhPerYear);

                calculatedTariffs.Add(
                    new CalculationResult()
                    {
                        TariffName = electricityTariff.Name,
                        AnnualCost = annualCost
                    });
            }

            calculatedTariffs = calculatedTariffs.OrderBy(x => x.AnnualCost).ToList();

            return calculatedTariffs;
        }
    }
}