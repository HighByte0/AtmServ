using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
