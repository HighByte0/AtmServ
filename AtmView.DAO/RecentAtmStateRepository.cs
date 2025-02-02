using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class RecentAtmStateRepository : GenericRepository<RecentAtmState, int>, IRecentAtmStateRepository
    {
        public RecentAtmStateRepository(AtmViewContext context)
            : base(context)
        {

        }


    }
}
