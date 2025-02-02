using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AtmRemarqueService : EntityService<AtmRemarque, int>, IAtmRemarqueService
    {
        IUnitOfWork _unitOfWork;
        IAtmRemarqueRepository _atmRemarqueRepository;

        public AtmRemarqueService(IUnitOfWork unitOfWork, IAtmRemarqueRepository atmRemarqueRepository)
            : base(unitOfWork, atmRemarqueRepository)
        {
            _unitOfWork = unitOfWork;
            _atmRemarqueRepository = atmRemarqueRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
