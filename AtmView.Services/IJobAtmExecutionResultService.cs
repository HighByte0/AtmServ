using AtmView.Entities;

namespace AtmView.Services
{
    public interface IJobAtmExecutionResultService : IEntityService<JobAtmExecutionResult, int>
    {
        void Commit();
    }
}
