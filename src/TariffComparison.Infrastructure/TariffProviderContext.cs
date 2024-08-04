using LiteDB;
using Microsoft.Extensions.Options;
using TariffComparison.Domain.Entities;
using TariffComparison.Infrastructure.Options;

namespace TariffComparison.Infrastructure
{
    public class TariffProviderContext : IDisposable
    {
        private readonly TariffProviderDatabaseSettings _settings;
        public LiteDatabase Database { get; private set; }

        public TariffProviderContext(IOptions<TariffProviderDatabaseSettings> settings)
        {
            _settings = settings.Value;
            Database = new LiteDatabase(new MemoryStream());
            // in-memory to solve modifying file permission error for Docker container,
            // so the user doesn't require root access
            /*     
                Database = new LiteDatabase(
                new ConnectionString
                {
                    Filename = $"{_settings.DatabaseName}.db",
                    Connection = ConnectionType.Shared,
                });*/
        }

        public ILiteCollection<ElectricityTariff> ElectricityTariffs => Database.GetCollection<ElectricityTariff>(_settings.TariffsCollectionName);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // workaround to not dispose of the in-memory database after the first request
            // for demo purpose
            /*if (disposing)
             {
                 Database.Dispose();
             }*/
        }
    }
}