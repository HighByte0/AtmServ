using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class StateTypeService : EntityService<StateType, int>, IStateTypeService
    {
        IUnitOfWork _unitOfWork;
        IStateTypeRepository _stateTypeRepository;

        public StateTypeService(IUnitOfWork unitOfWork, IStateTypeRepository stateTypeRepository)
            : base(unitOfWork, stateTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _stateTypeRepository = stateTypeRepository;

        }

    }
}
