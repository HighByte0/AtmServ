using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public interface IRegionRepository : IGenericRepository<Region, int>
    {

        void DeleteRegion(int regionId, string ConnectionString);
    }
}
