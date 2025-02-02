using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AtmRejectStatutService : EntityService<AtmRejectStatut, int>, IAtmRejectStatutService
    {
        IUnitOfWork _unitOfWork;
        IAtmRejectStatutRepository _AtmRejectStatutRepository;

        public AtmRejectStatutService(IUnitOfWork unitOfWork, IAtmRejectStatutRepository AtmRejectStatutRepository)
            : base(unitOfWork, AtmRejectStatutRepository)
        {
            _unitOfWork = unitOfWork;
            _AtmRejectStatutRepository = AtmRejectStatutRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
