using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class FactorDepositAgencyRepository : GenericRepository<FactorDepositrAgency, int>, IFactorDepositAgencyRepository
    {
        public FactorDepositAgencyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
