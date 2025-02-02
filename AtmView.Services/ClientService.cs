using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ClientService : EntityService<Client, int>, IClientService
    {
        IUnitOfWork _unitOfWork;
        IClientRepository _ClientRepository;

        public ClientService(IUnitOfWork unitOfWork, IClientRepository ClientRepository)
            : base(unitOfWork, ClientRepository)
        {
            _unitOfWork = unitOfWork;
            _ClientRepository = ClientRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }

}
