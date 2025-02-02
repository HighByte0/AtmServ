using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CashProviderService : EntityService<CashProvider, int>, ICashProviderService
    {
        IUnitOfWork _unitOfWork;
        ICashProviderRepository _cashProviderRepository;

        public CashProviderService(IUnitOfWork unitOfWork, ICashProviderRepository cashProviderRepository)
            : base(unitOfWork, cashProviderRepository)
        {
            _unitOfWork = unitOfWork;
            _cashProviderRepository = cashProviderRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
