using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JobAtmExecutionResultService : EntityService<JobAtmExecutionResult, int>, IJobAtmExecutionResultService
    {
        IUnitOfWork _unitOfWork;
        IJobAtmExecutionResultRepository _JobAtmExecutionResultRepository;

        public JobAtmExecutionResultService(IUnitOfWork unitOfWork, IJobAtmExecutionResultRepository JobAtmExecutionResultRepository)
            : base(unitOfWork, JobAtmExecutionResultRepository)
        {
            _unitOfWork = unitOfWork;
            _JobAtmExecutionResultRepository = JobAtmExecutionResultRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
