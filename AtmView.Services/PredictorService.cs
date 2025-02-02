using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class PredictorService : EntityService<Predictor, int>, IPredictorService
    {
        IUnitOfWork _unitOfWork;
        IPredictorRepository _predictorRepository;

        public PredictorService(IUnitOfWork unitOfWork, IPredictorRepository predictorRepository)
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
