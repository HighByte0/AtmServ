using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICityService : IEntityService<City, int>
    {

        void Commit();
        void DeleteCity(int cityId, string ConnectionString);
    }
}
