namespace TariffComparison.Domain.Entities
{
    public class ElectricityTariff : Tariff
    {
        public double AdditionalKwhCost { get; set; }
        public double IncludedKwh { get; set; }
    }
}