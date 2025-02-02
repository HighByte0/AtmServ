
using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ErrorTypeService : EntityService<ErrorType, int>, IErrorTypeService
    {
        IUnitOfWork _unitOfWork;
        IErrorTypeRepository _ErrorTypeRepository;

        public ErrorTypeService(IUnitOfWork unitOfWork, IErrorTypeRepository ErrorTypeRepository)
            : base(unitOfWork, ErrorTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _ErrorTypeRepository = ErrorTypeRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
