namespace TariffComparison.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ITariffRepository TariffRepository { get; }
    }
}