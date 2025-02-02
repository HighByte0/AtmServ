using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class FactorRepository : GenericRepository<Factor, int>, IFactorRepository
    {
        public FactorRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
