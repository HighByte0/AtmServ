using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ResultService : EntityService<Result, int>, IResultService
    {
        IUnitOfWork _unitOfWork;
        IResultRepository _ResultRepository;

        public ResultService(IUnitOfWork unitOfWork, IResultRepository ResultRepository)
            : base(unitOfWork, ResultRepository)
        {
            _unitOfWork = unitOfWork;
            _ResultRepository = ResultRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
