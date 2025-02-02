using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugStatutRepository : GenericRepository<BugStatut, int>, IBugStatutRepository
    {
        public BugStatutRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

