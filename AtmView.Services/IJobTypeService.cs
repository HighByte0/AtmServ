using AtmView.Entities;

namespace AtmView.Services
{
    public interface IJobTypeService : IEntityService<JobType, int>
    {

        void Commit();
    }
}
