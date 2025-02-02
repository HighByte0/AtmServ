using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BankService : EntityService<Bank, int>, IBankService
    {
        IUnitOfWork _unitOfWork;
        IBankRepository _bankRepository;

        public BankService(IUnitOfWork unitOfWork, IBankRepository bankRepository)
            : base(unitOfWork, bankRepository)
        {
            _unitOfWork = unitOfWork;
            _bankRepository = bankRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void DeleteBank(int bankId, string ConnectionString)
        {
            _bankRepository.DeleteBank(bankId, ConnectionString);
        }
    }
}
