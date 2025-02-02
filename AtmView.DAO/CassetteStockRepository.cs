using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class CassetteStockRepository : GenericRepository<CassetteStock, int>, ICassetteStockRepository
    {
        public CassetteStockRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
