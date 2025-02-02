using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class DoneOrderAgencyService : EntityService<DoneOrderAgency, int>, IDoneOrderAgencyService
    {
        IUnitOfWork _unitOfWork;
        IDoneOrderAgencyRepository _doneOrderRepository;

        public DoneOrderAgencyService(IUnitOfWork unitOfWork, IDoneOrderAgencyRepository doneOrderRepository)
            : base(unitOfWork, doneOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _doneOrderRepository = doneOrderRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
