using AtmView.Entities;

namespace AtmView.Services
{
    public interface IStateJobService : IEntityService<StateJob, int>
    {
        void Commit();
    }
}
