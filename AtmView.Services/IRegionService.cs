using AtmView.Entities;

namespace AtmView.Services
{
    public interface IRegionService : IEntityService<Region, int>
    {
        void Commit();
        void DeleteRegion(int regionId, string ConnectionString);
    }
}
