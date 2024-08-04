namespace TariffComparison.Infrastructure.Options
{
    public class TariffProviderDatabaseSettings
    {
        public string DatabaseName { get; set; } = String.Empty;
        public string TariffsCollectionName { get; set; } = String.Empty;
    }
}