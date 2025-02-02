using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Services
{
    public class OrderAgencyService : EntityService<OrderAgency, int>, IOrderAgencyService
    {
        IUnitOfWork _unitOfWork;
        IOrderAgencyRepository _orderAgencyRepository;

        public OrderAgencyService(IUnitOfWork unitOfWork, IOrderAgencyRepository orderAgencyRepository)
            : base(unitOfWork, orderAgencyRepository)
        {
            _unitOfWork = unitOfWork;
            _orderAgencyRepository = orderAgencyRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
