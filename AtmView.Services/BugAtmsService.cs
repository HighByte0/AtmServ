
using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugAtmsService : EntiteService<BugAtm>, IBugAtmsService
    {
        IUnitOfWork _unitOfWork;
        IBugAtmsRepository _BugAtmsRepository;

        public BugAtmsService(IUnitOfWork unitOfWork, IBugAtmsRepository BugAtmsRepository)
            : base(unitOfWork, BugAtmsRepository)
        {
            _unitOfWork = unitOfWork;
            _BugAtmsRepository = BugAtmsRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }



}
