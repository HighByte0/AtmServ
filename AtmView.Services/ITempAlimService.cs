using AtmView.Entities;

namespace AtmView.Services
{
    public interface ITempAlimService : IEntityService<TempAlim, int>
    {
        void Commit();
    }
}
