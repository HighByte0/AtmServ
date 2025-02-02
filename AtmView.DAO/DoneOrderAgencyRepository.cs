using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class DoneOrderAgencyRepository : GenericRepository<DoneOrderAgency, int>, IDoneOrderAgencyRepository
    {
        public DoneOrderAgencyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
