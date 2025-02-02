using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugPriorityService : EntityService<BugPriority, int>, IBugPriorityService
    {
        IUnitOfWork _unitOfWork;
        IBugPriorityRepository _bugPriorityRepository;

        public BugPriorityService(IUnitOfWork unitOfWork, IBugPriorityRepository bugPriorityRepository)
            : base(unitOfWork, bugPriorityRepository)
        {
            _unitOfWork = unitOfWork;
            _bugPriorityRepository = bugPriorityRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
