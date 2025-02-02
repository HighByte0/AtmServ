using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugRepository : GenericRepository<Bug, int>, IBugRepository
    {
        public BugRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
