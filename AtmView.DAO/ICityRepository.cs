using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public interface ICityRepository : IGenericRepository<City, int>
    {
        void DeleteCity(int cityId, string ConnectionString);
    }
}
