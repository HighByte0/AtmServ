using AtmView.Entities;

namespace AtmView.Services
{
    public interface IJobCommandService : IEntiteService<JobCommand>
    {
        void Commit();
    }
}
