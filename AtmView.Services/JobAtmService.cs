using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class JobAtmService : EntiteService<JobAtm>, IJobAtmService
    {
        IUnitOfWork _unitOfWork;
        IJobAtmRepository _jobAtmRepository;

        public JobAtmService(IUnitOfWork unitOfWork, IJobAtmRepository jobAtmRepository)
            : base(unitOfWork, jobAtmRepository)
        {
            _unitOfWork = unitOfWork;
            _jobAtmRepository = jobAtmRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


    }



}
