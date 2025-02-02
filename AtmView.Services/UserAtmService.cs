using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class UserAtmService : EntiteService<UserAtm>, IUserAtmService
    {
        IUnitOfWork _unitOfWork;
        IUserAtmRepository _userAtmRepository;

        public UserAtmService(IUnitOfWork unitOfWork, IUserAtmRepository userAtmRepository)
            : base(unitOfWork, userAtmRepository)
        {
            _unitOfWork = unitOfWork;
            _userAtmRepository = userAtmRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
