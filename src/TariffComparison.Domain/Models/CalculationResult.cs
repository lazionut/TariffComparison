namespace TariffComparison.Domain.Models
{
    public class CalculationResult
    {
        public string TariffName { get; set; } = null!;
        public double AnnualCost { get; set; }
    }
}