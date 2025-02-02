using AtmView.Entities;

namespace AtmView.Services
{
    public interface IDoneOrderService : IEntityService<DoneOrder, int>
    {
        void Commit();
    }
}
