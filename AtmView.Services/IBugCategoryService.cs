using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugCategoryService : IEntityService<BugCategory, int>
    {
        void Commit();
    }
}
