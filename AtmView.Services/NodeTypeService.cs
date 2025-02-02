using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class NodeTypeService : EntityService<NodeType, int>, INodeTypeService
    {
        IUnitOfWork _unitOfWork;
        INodeTypeRepository _nodeTypeRepository;

        public NodeTypeService(IUnitOfWork unitOfWork, INodeTypeRepository nodeTypeRepository)
            : base(unitOfWork, nodeTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _nodeTypeRepository = nodeTypeRepository;

        }

    }
}
