using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class TreeViewDetailRepository : GenericRepository<TreeViewDetail, int>, ITreeViewDetailRepository
    {
        public TreeViewDetailRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

