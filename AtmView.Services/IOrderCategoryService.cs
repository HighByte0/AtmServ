using AtmView.Entities;

namespace AtmView.Services
{
    public interface IOrderCategoryService : IEntityService<OrderCategory, int>
    {
        void Commit();
    }
}
