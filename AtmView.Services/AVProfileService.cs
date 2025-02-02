using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AVProfileService : EntityService<AtmProfile, int>, IAVProfileService
    {
        IUnitOfWork _unitOfWork;
        IAVProfileRepository _avprofileRepository;

        public AVProfileService(IUnitOfWork unitOfWork, IAVProfileRepository avprofileRepository)
            : base(unitOfWork, avprofileRepository)
        {
            _unitOfWork = unitOfWork;
            _avprofileRepository = avprofileRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
