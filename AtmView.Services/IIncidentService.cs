using AtmView.Entities;

namespace AtmView.Services
{
    public interface IIncidentService : IEntityService<Incident, int>
    {
        void Commit();
    }
}
