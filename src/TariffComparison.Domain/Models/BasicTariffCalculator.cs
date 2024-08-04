namespace TariffComparison.Domain.Models
{
    public class BasicTariffCalculator : TariffCalculator
    {
        private readonly double _baseCost;
        private readonly double _additionalKwhCost;

        private const int monthsPerYear = 12;
        private const double centsToEuro = 0.01;

        public BasicTariffCalculator(double baseCost, double additionalKwhCost)
        {
            _baseCost = baseCost;
            _additionalKwhCost = additionalKwhCost;
        }

        public override double CalculateAnnualCosts(double consumption)
        {
            return _baseCost * monthsPerYear + Math.Round(_additionalKwhCost * consumption * centsToEuro, 2);
        }
    }
}