using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class TempAlimService : EntityService<TempAlim, int>, ITempAlimService
    {
        IUnitOfWork _unitOfWork;
        ITempAlimRepository _tempAlimRepository;

        public TempAlimService(IUnitOfWork unitOfWork, ITempAlimRepository tempAlimRepository)
            : base(unitOfWork, tempAlimRepository)
        {
            _unitOfWork = unitOfWork;
            _tempAlimRepository = tempAlimRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
