using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CashPointService : EntityService<CashPoint, int>, ICashPointService
    {
        IUnitOfWork _unitOfWork;
        ICashPointRepository _CashPointRepository;

        public CashPointService(IUnitOfWork unitOfWork, ICashPointRepository CashPointRepository)
            : base(unitOfWork, CashPointRepository)
        {
            _unitOfWork = unitOfWork;
            _CashPointRepository = CashPointRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void DeleteCashPoint(int CashPointId, string ConnectionString)
        {
            _CashPointRepository.DeleteCashPoint(CashPointId, ConnectionString);
        }

    }
}
