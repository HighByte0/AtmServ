using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugStatutService : EntityService<BugStatut, int>, IBugStatutService
    {
        IUnitOfWork _unitOfWork;
        IBugStatutRepository _bugStatutRepository;

        public BugStatutService(IUnitOfWork unitOfWork, IBugStatutRepository bugStatutRepository)
            : base(unitOfWork, bugStatutRepository)
        {
            _unitOfWork = unitOfWork;
            _bugStatutRepository = bugStatutRepository;
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
