using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class BugCategoryService : EntityService<BugCategory, int>, IBugCategoryService
    {
        IUnitOfWork _unitOfWork;
        IBugCategoryRepository _bugCategoryRepository;

        public BugCategoryService(IUnitOfWork unitOfWork, IBugCategoryRepository bugCategoryRepository)
            : base(unitOfWork, bugCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _bugCategoryRepository = bugCategoryRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
