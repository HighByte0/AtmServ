using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class StateJobRepository : GenericRepository<StateJob, int>, IStateJobRepository
    {
        public StateJobRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
