using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CashStockService : EntityService<CashStock, int>, ICashStockService
    {
        IUnitOfWork _unitOfWork;
        ICashStockRepository _cashStockRepository;

        public CashStockService(IUnitOfWork unitOfWork, ICashStockRepository cashStockRepository)
            : base(unitOfWork, cashStockRepository)
        {
            _unitOfWork = unitOfWork;
            _cashStockRepository = cashStockRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
