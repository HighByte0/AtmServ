using AtmView.Entities;

namespace AtmView.Services
{
    public interface IJobAtmService : IEntiteService<JobAtm>
    {
        void Commit();
    }
}
