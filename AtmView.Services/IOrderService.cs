using AtmView.Entities;

namespace AtmView.Services
{
    public interface IOrderService : IEntityService<Order, int>
    {
        void Commit();
    }
}
