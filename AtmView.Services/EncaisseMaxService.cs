using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class EncaisseMaxService : EntityService<EncaisseMax, int>, IEncaisseMaxService
    {
        IUnitOfWork _unitOfWork;
        IEncaisseMaxRepository _EncaisseMaxRepository;

        public EncaisseMaxService(IUnitOfWork unitOfWork, IEncaisseMaxRepository EncaisseMaxRepository)
            : base(unitOfWork, EncaisseMaxRepository)
        {
            _unitOfWork = unitOfWork;
            _EncaisseMaxRepository = EncaisseMaxRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
