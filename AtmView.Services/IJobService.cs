using AtmView.Entities;

namespace AtmView.Services
{
    public interface IJobService : IEntityService<Job, int>
    {
        void Commit();
    }
}
