using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ApplicationService : EntityService<Application, int>, IApplicationService
    {
        IUnitOfWork _unitOfWork;
        IApplicationRepository _applicationRepository;

        public ApplicationService(IUnitOfWork unitOfWork, IApplicationRepository applicationRepository)
            : base(unitOfWork, applicationRepository)
        {
            _unitOfWork = unitOfWork;
            _applicationRepository = applicationRepository;

        }

    }
}
