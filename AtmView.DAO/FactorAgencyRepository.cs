using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class FactorAgencyRepository : GenericRepository<FactorAgency, int>, IFactorAgencyRepository
    {
        public FactorAgencyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
