using TariffComparison.Application.Interfaces;

namespace TariffComparison.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TariffProviderContext _tariffProviderContext;

        public UnitOfWork(TariffProviderContext tariffProviderContext, ITariffRepository tariffRepository)
        {
            _tariffProviderContext = tariffProviderContext;
            TariffRepository = tariffRepository;
        }

        public ITariffRepository TariffRepository { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tariffProviderContext.Dispose();
            }
        }
    }
}