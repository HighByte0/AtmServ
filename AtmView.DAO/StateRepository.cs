using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class StateRepository : GenericRepository<State, int>, IStateRepository
    {
        public StateRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

