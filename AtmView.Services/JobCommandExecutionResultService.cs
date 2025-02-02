using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JobCommandExecutionResultService : EntityService<JobCommandExecutionResult, int>, IJobCommandExecutionResultService
    {
        IUnitOfWork _unitOfWork;
        IJobCommandExecutionResultRepository _jobCommandExecutionResultRepository;

        public JobCommandExecutionResultService(IUnitOfWork unitOfWork, IJobCommandExecutionResultRepository jobCommandExecutionResultRepository)
            : base(unitOfWork, jobCommandExecutionResultRepository)
        {
            _unitOfWork = unitOfWork;
            _jobCommandExecutionResultRepository = jobCommandExecutionResultRepository;

        }

    }
}
