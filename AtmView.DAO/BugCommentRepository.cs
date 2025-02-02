using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugCommentRepository : GenericRepository<BugComment, int>, IBugCommentRepository
    {
        public BugCommentRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
