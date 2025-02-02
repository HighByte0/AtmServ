using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugHistoryService : EntityService<BugHistory, int>, IBugHistoryService
    {
        IUnitOfWork _unitOfWork;
        IBugHistoryRepository _bugHistoryRepository;

        public BugHistoryService(IUnitOfWork unitOfWork, IBugHistoryRepository bugHistoryRepository)
            : base(unitOfWork, bugHistoryRepository)
        {
            _unitOfWork = unitOfWork;
            _bugHistoryRepository = bugHistoryRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Remove(BugHistory bugHistory)
        {
            _bugHistoryRepository.Delete(bugHistory);

        }
    }
}
