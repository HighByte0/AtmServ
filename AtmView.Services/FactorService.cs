using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class FactorService : EntityService<Factor, int>, IFactorService
    {
        IUnitOfWork _unitOfWork;
        IFactorRepository _FactorRepository;

        public FactorService(IUnitOfWork unitOfWork, IFactorRepository FactorRepository)
            : base(unitOfWork, FactorRepository)
        {
            _unitOfWork = unitOfWork;
            _FactorRepository = FactorRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
