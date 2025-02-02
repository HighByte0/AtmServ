using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class CashStockRepository : GenericRepository<CashStock, int>, ICashStockRepository
    {
        public CashStockRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
