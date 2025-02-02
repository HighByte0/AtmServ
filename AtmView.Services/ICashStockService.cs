using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICashStockService : IEntityService<CashStock, int>
    {
        void Commit();
    }
}
