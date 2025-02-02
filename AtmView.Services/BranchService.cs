using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BranchService : EntityService<Branch, int>, IBranchService
    {
        IUnitOfWork _unitOfWork;
        IBranchRepository _branchRepository;

        public BranchService(IUnitOfWork unitOfWork, IBranchRepository branchRepository)
            : base(unitOfWork, branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void DeleteBranch(int branchId, string ConnectionString)
        {
            _branchRepository.DeleteBranch(branchId, ConnectionString);
        }

    }
}
