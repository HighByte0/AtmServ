using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JobTypeService : EntityService<JobType, int>, IJobTypeService
    {
        IUnitOfWork _unitOfWork;
        IJobTypeRepository _jobTypeRepository;

        public JobTypeService(IUnitOfWork unitOfWork, IJobTypeRepository jobTypeRepository)
            : base(unitOfWork, jobTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _jobTypeRepository = jobTypeRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
