using TariffComparison.Domain.Entities;
using TariffComparison.Domain.Enums;
using TariffComparison.Domain.Models;

namespace TariffComparison.Domain.Helpers
{
    public class TariffCalculatorGenerator
    {
        public TariffCalculator Generate(ElectricityTariff tariff)
        {
            switch (tariff.Type)
            {
                case TariffType.BasicElectricityTariff:
                    return new BasicTariffCalculator(tariff.BaseCost, tariff.AdditionalKwhCost);

                case TariffType.PackagedTariff:
                    return new PackagedTariffCalculator(tariff.BaseCost, tariff.IncludedKwh, tariff.AdditionalKwhCost);

                default:
                    throw new ArgumentOutOfRangeException($"Unsupported tariff type: {tariff.Type}");
            }
        }
    }
}