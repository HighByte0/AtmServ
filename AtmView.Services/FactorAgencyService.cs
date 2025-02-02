using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class FactorAgencyService : EntityService<FactorAgency, int>, IFactorAgencyService
    {
        IUnitOfWork _unitOfWork;
        IFactorAgencyRepository _FactorAgencyRepository;

        public FactorAgencyService(IUnitOfWork unitOfWork, IFactorAgencyRepository FactorAgencyRepository)
            : base(unitOfWork, FactorAgencyRepository)
        {
            _unitOfWork = unitOfWork;
            _FactorAgencyRepository = FactorAgencyRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
