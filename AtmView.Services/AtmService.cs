using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AtmService : EntityService<Atm, string>, IAtmService  //IAtmService ,EntityService<Atm, string>
    {
        IUnitOfWork _unitOfWork;
        IAtmRepository _atmRepository;

        public AtmService(IUnitOfWork unitOfWork, IAtmRepository atmRepository)
        : base(unitOfWork, atmRepository)
        {
            _unitOfWork = unitOfWork;
            _atmRepository = atmRepository;

        }


        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void DeleteAtm(string atmId, string ConnectionString)
        {
            _atmRepository.DeleteAtm(atmId, ConnectionString);
        }
    }
}
