using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JobCommandService : EntiteService<JobCommand>, IJobCommandService
    {
        IUnitOfWork _unitOfWork;
        IJobCommandRepository _jobCommandRepository;

        public JobCommandService(IUnitOfWork unitOfWork, IJobCommandRepository jobCommandRepository)
            : base(unitOfWork, jobCommandRepository)
        {
            _unitOfWork = unitOfWork;
            _jobCommandRepository = jobCommandRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }



}
