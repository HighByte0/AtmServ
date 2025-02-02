using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class TransationTypeService : EntityService<TransationType, int>, ITransationTypeService
    {
        IUnitOfWork _unitOfWork;
        ITransationTypeRepository _transactionTypeRepository;

        public TransationTypeService(IUnitOfWork unitOfWork, ITransationTypeRepository transactionTypeRepository)
            : base(unitOfWork, transactionTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionTypeRepository = transactionTypeRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }




    }
}
