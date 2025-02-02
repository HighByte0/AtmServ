using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ActionCorrectiveService : EntityService<ActionCorrective, int>, IActionCorrectiveService
    {
        IUnitOfWork _unitOfWork;
        IActionCorrectiveRepository _actionCorrectiveRepository;

        public ActionCorrectiveService(IUnitOfWork unitOfWork, IActionCorrectiveRepository actionCorrectiveRepository)
            : base(unitOfWork, actionCorrectiveRepository)
        {
            _unitOfWork = unitOfWork;
            _actionCorrectiveRepository = actionCorrectiveRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
