using TariffComparison.Domain.Entities;

namespace TariffComparison.Application.Interfaces
{
    public interface ITariffRepository
    {
        Task<IEnumerable<ElectricityTariff>> GetAll();
    }
}