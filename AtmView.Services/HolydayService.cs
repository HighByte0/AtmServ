using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class HolydayService : EntityService<Holyday, int>, IHolydayService
    {
        IUnitOfWork _unitOfWork;
        IHolydayRepository _holidayRepository;

        public HolydayService(IUnitOfWork unitOfWork, IHolydayRepository holidayRepository)
            : base(unitOfWork, holidayRepository)
        {
            _unitOfWork = unitOfWork;
            _holidayRepository = holidayRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
