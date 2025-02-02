using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class ContextualMenuService : EntityService<ContextualMenu, int>, IContextualMenuService
    {
        IUnitOfWork _unitOfWork;
        IContextualMenuRepository _contextualMenuRepository;

        public ContextualMenuService(IUnitOfWork unitOfWork, IContextualMenuRepository contextualMenuRepository)
            : base(unitOfWork, contextualMenuRepository)
        {
            _unitOfWork = unitOfWork;
            _contextualMenuRepository = contextualMenuRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
