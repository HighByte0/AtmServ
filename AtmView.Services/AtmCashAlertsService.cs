
using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AtmCashAlertsService : EntityService<AtmCashAlert, int>, IAtmCashAlertsService
    {
        IUnitOfWork _unitOfWork;
        IAtmCashAlertsRepository _AtmCashAlertsRepository;

        public AtmCashAlertsService(IUnitOfWork unitOfWork, IAtmCashAlertsRepository AtmCashAlertsRepository)
            : base(unitOfWork, AtmCashAlertsRepository)
        {
            _unitOfWork = unitOfWork;
            _AtmCashAlertsRepository = AtmCashAlertsRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
