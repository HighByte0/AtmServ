
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AtmCashAlertsRepository : GenericRepository<AtmCashAlert, int>, IAtmCashAlertsRepository
    {
        public AtmCashAlertsRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
