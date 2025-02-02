using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class TreeViewDetailService : EntityService<TreeViewDetail, int>, ITreeViewDetailService
    {
        IUnitOfWork _unitOfWork;
        ITreeViewDetailRepository _treeViewDetailRepository;

        public TreeViewDetailService(IUnitOfWork unitOfWork, ITreeViewDetailRepository treeViewDetailRepository)
            : base(unitOfWork, treeViewDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _treeViewDetailRepository = treeViewDetailRepository;

        }

    }
}
