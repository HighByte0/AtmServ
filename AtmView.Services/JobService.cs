using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JobService : EntityService<Job, int>, IJobService
    {
        IUnitOfWork _unitOfWork;
        IJobRepository _jobRepository;

        public JobService(IUnitOfWork unitOfWork, IJobRepository jobRepository)
            : base(unitOfWork, jobRepository)
        {
            _unitOfWork = unitOfWork;
            _jobRepository = jobRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }
}
