using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class NodeTypeRepository : GenericRepository<NodeType, int>, INodeTypeRepository
    {
        public NodeTypeRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
