using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class DoneOrderService : EntityService<DoneOrder, int>, IDoneOrderService
    {
        IUnitOfWork _unitOfWork;
        IDoneOrderRepository _doneOrderRepository;

        public DoneOrderService(IUnitOfWork unitOfWork, IDoneOrderRepository doneOrderRepository)
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
