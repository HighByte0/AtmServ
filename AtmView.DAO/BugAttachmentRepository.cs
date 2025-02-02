using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class BugAttachmentRepository : GenericRepository<BugAttachment, int>, IBugAttachmentRepository
    {
        public BugAttachmentRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
