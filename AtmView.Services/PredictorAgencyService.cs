using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class PredictorAgencySevice : EntityService<PredictorAgency, int>, IPredictorAgencyService
    {
        IUnitOfWork _unitOfWork;
        IPredictorAgencyRepository _predictorAgencyRepository;

        public PredictorAgencySevice(IUnitOfWork unitOfWork, IPredictorAgencyRepository predictorAgencyRepository)
            : base(unitOfWork, predictorAgencyRepository)
        {
            _unitOfWork = unitOfWork;
            _predictorAgencyRepository = predictorAgencyRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
