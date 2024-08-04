using TariffComparison.Domain.Entities;
using TariffComparison.Domain.Enums;

namespace TariffComparison.Infrastructure
{
    public class Seeder
    {
        private readonly TariffProviderContext _context;

        public Seeder(TariffProviderContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            var firstElectricityTariff = new ElectricityTariff { Name = "Product A", Type = TariffType.BasicElectricityTariff, BaseCost = 5, AdditionalKwhCost = 22 };
            var secondElectricityTariff = new ElectricityTariff { Name = "Product B", Type = TariffType.PackagedTariff, IncludedKwh = 4000, BaseCost = 800, AdditionalKwhCost = 30 };

            _context.ElectricityTariffs.Insert(firstElectricityTariff);
            _context.ElectricityTariffs.Insert(secondElectricityTariff);
        }
    }
}