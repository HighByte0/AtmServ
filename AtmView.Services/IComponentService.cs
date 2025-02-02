using AtmView.Entities;

namespace AtmView.Services
{
    public interface IComponentService : IEntityService<Component, int>
    {
        void Commit();
    }
}
