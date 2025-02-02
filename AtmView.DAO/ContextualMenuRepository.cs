using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ContextualMenuRepository : GenericRepository<ContextualMenu, int>, IContextualMenuRepository
    {
        public ContextualMenuRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
