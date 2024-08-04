namespace TariffComparison.Domain.Models
{
    public abstract class TariffCalculator
    {
        public abstract double CalculateAnnualCosts(double consumption);
    }
}