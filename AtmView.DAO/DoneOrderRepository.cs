using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class DoneOrderRepository : GenericRepository<DoneOrder, int>, IDoneOrderRepository
    {
        public DoneOrderRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
