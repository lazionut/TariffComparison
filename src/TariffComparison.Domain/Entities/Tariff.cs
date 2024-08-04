using TariffComparison.Domain.Enums;

namespace TariffComparison.Domain.Entities
{
    public abstract class Tariff
    {
        public string Name { get; set; } = null!;
        public TariffType Type { get; set; }
        public double BaseCost { get; set; }
    }
}