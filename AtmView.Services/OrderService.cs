using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class OrderService : EntityService<Order, int>, IOrderService
    {
        IUnitOfWork _unitOfWork;
        IOrderRepository _orderRepository;

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
            : base(unitOfWork, orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
