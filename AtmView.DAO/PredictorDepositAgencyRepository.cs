using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class PredictorDepositAgencyRepository : GenericRepository<PredictorDepositAgency, int>, IPredictorDepositAgencyRepository
    {
        public PredictorDepositAgencyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
