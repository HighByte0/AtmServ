using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class OrderCategoryService : EntityService<OrderCategory, int>, IOrderCategoryService
    {
        IUnitOfWork _unitOfWork;
        IOrderCategoryRepository _orderCategoryRepository;

        public OrderCategoryService(IUnitOfWork unitOfWork, IOrderCategoryRepository orderCategoryRepository)
            : base(unitOfWork, orderCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _orderCategoryRepository = orderCategoryRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
