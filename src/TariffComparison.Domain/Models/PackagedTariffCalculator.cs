namespace TariffComparison.Domain.Models
{
    public class PackagedTariffCalculator : TariffCalculator
    {
        private readonly double _baseCost;
        private readonly double _includedKwh;
        private readonly double _additionalKwhCost;

        private const double centsToEuro = 0.01;

        public PackagedTariffCalculator(double baseCost, double includedKwh, double additionalKwhCost)
        {
            _baseCost = baseCost;
            _includedKwh = includedKwh;
            _additionalKwhCost = additionalKwhCost;
        }

        public override double CalculateAnnualCosts(double consumption)
        {
            if (consumption <= _includedKwh)
            {
                return _baseCost;
            }
            else
            {
                var additionalConsumption = consumption - _includedKwh;
                return _baseCost + Math.Round(additionalConsumption * _additionalKwhCost * centsToEuro, 2);
            }
        }
    }
}