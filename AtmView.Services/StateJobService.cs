using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class StateJobService : EntityService<StateJob, int>, IStateJobService
    {
        IUnitOfWork _unitOfWork;
        IStateJobRepository _stateJobRepository;

        public StateJobService(IUnitOfWork unitOfWork, IStateJobRepository stateJobRepository)
            : base(unitOfWork, stateJobRepository)
        {
            _unitOfWork = unitOfWork;
            _stateJobRepository = stateJobRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
