using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class FactorDepositAgencyService : EntityService<FactorDepositrAgency, int>, IFactorDepositAgencyService
    {
        IUnitOfWork _unitOfWork;
        IFactorDepositAgencyRepository _FactorDepositAgencyRepository;

        public FactorDepositAgencyService(IUnitOfWork unitOfWork, IFactorDepositAgencyRepository FactorDepositAgencyRepository)
            : base(unitOfWork, FactorDepositAgencyRepository)
        {
            _unitOfWork = unitOfWork;
            _FactorDepositAgencyRepository = FactorDepositAgencyRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
