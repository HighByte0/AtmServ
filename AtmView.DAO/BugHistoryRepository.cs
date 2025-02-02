using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugHistoryRepository : GenericRepository<BugHistory, int>, IBugHistoryRepository
    {
        public BugHistoryRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
