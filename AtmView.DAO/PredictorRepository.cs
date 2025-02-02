using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class PredictorRepository : GenericRepository<Predictor, int>, IPredictorRepository
    {
        public PredictorRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
