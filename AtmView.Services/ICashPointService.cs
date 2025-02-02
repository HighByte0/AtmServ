using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICashPointService : IEntityService<CashPoint, int>
    {
        void Commit();
        void DeleteCashPoint(int CashPointId, string ConnectionString);
    }
}
