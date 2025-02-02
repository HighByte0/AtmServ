using AtmView.Entities;

namespace AtmView.Services
{
    public interface IContextualMenuService : IEntityService<ContextualMenu, int>
    {
        void Commit();
    }
}
