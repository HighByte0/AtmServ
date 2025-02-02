using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CassetteStockService : EntityService<CassetteStock, int>, ICassetteStockService
    {
        IUnitOfWork _unitOfWork;
        ICassetteStockRepository _cassetteStockRepository;

        public CassetteStockService(IUnitOfWork unitOfWork, ICassetteStockRepository cassetteStockRepository)
            : base(unitOfWork, cassetteStockRepository)
        {
            _unitOfWork = unitOfWork;
            _cassetteStockRepository = cassetteStockRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
