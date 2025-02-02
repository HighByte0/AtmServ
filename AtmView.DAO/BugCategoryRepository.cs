using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugCategoryRepository : GenericRepository<BugCategory, int>, IBugCategoryRepository
    {
        public BugCategoryRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

