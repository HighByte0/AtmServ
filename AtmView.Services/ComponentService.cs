using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ComponentService : EntityService<Component, int>, IComponentService
    {
        IUnitOfWork _unitOfWork;
        IComponentRepository _componentRepository;

        public ComponentService(IUnitOfWork unitOfWork, IComponentRepository componentRepository)
            : base(unitOfWork, componentRepository)
        {
            _unitOfWork = unitOfWork;
            _componentRepository = componentRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
