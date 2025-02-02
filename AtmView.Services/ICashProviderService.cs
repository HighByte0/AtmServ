using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICashProviderService : IEntityService<CashProvider, int>
    {
        void Commit();
    }
}
