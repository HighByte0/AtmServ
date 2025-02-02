using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AtmCommErrorService : EntityService<AtmCommError, int>, IAtmCommErrorService
    {
        IUnitOfWork _unitOfWork;
        IAtmCommErrorRepository _AtmCommErrorRepository;

        public AtmCommErrorService(IUnitOfWork unitOfWork, IAtmCommErrorRepository AtmCommErrorRepository)
            : base(unitOfWork, AtmCommErrorRepository)
        {
            _unitOfWork = unitOfWork;
            _AtmCommErrorRepository = AtmCommErrorRepository;

        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
