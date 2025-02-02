using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class OrderCategoryRepository : GenericRepository<OrderCategory, int>, IOrderCategoryRepository
    {
        public OrderCategoryRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

