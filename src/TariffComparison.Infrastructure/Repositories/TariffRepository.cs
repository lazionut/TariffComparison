using TariffComparison.Application.Interfaces;
using TariffComparison.Domain.Entities;

namespace TariffComparison.Infrastructure.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly TariffProviderContext _context;

        public TariffRepository(TariffProviderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ElectricityTariff>> GetAll()
        {
            return await Task.FromResult(_context.ElectricityTariffs.FindAll());
        }
    }
}