using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugCommentService : EntityService<BugComment, int>, IBugCommentService
    {
        IUnitOfWork _unitOfWork;
        IBugCommentRepository _bugCommentRepository;

        public BugCommentService(IUnitOfWork unitOfWork, IBugCommentRepository bugCommentRepository)
            : base(unitOfWork, bugCommentRepository)
        {
            _unitOfWork = unitOfWork;
            _bugCommentRepository = bugCommentRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Remove(BugComment bugComment)
        {
            _bugCommentRepository.Delete(bugComment);

        }
    }
}
