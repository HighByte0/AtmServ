using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class PredictorAgencyRepository : GenericRepository<PredictorAgency, int>, IPredictorAgencyRepository
    {
        public PredictorAgencyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
