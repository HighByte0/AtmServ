using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public interface ICashPointRepository : IGenericRepository<CashPoint, int>
    {
        void DeleteCashPoint(int CashPointId, string ConnectionString);
    }
}
