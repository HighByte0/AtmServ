using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class StateTypeRepository : GenericRepository<StateType, int>, IStateTypeRepository
    {
        public StateTypeRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
