using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugService : EntityService<Bug, int>, IBugService
    {
        IUnitOfWork _unitOfWork;
        IBugRepository _bugRepository;

        public BugService(IUnitOfWork unitOfWork, IBugRepository bugRepository)
            : base(unitOfWork, bugRepository)
        {
            _unitOfWork = unitOfWork;
            _bugRepository = bugRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
