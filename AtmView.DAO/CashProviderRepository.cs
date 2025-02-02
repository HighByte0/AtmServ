using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class CashProviderRepository : GenericRepository<CashProvider, int>, ICashProviderRepository
    {
        public CashProviderRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
