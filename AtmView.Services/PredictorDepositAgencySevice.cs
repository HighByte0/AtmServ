using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class PredictorDepositAgencySevice : EntityService<PredictorDepositAgency, int>, IPredictorDepositAgencyService
    {
        IUnitOfWork _unitOfWork;
        IPredictorDepositAgencyRepository _predictorRepository;

        public PredictorDepositAgencySevice(IUnitOfWork unitOfWork, IPredictorDepositAgencyRepository predictorRepository)
            : base(unitOfWork, predictorRepository)
        {
            _unitOfWork = unitOfWork;
            _predictorRepository = predictorRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
