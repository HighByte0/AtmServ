using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AtmErrorService : EntityService<AtmError, int>, IAtmErrorService
    {
        IUnitOfWork _unitOfWork;
        IAtmErrorRepository _atmErrorRepository;

        public AtmErrorService(IUnitOfWork unitOfWork, IAtmErrorRepository atmErrorRepository)
            : base(unitOfWork, atmErrorRepository)
        {
            _unitOfWork = unitOfWork;
            _atmErrorRepository = atmErrorRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
