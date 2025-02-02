using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CaisseAgenceService : EntityService<CaisseAgence, int>, ICaisseAgenceService
    {
        IUnitOfWork _unitOfWork;
        ICaisseAgenceRepository _branchRepository;

        public CaisseAgenceService(IUnitOfWork unitOfWork, ICaisseAgenceRepository branchRepository)
            : base(unitOfWork, branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }



    }
}
