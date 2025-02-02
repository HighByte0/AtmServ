using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugPriorityRepository : GenericRepository<BugPriority, int>, IBugPriorityRepository
    {
        public BugPriorityRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

