using AtmView.Entities;

namespace AtmView.Services
{
    public interface ICassetteStockService : IEntityService<CassetteStock, int>
    {
        void Commit();
    }
}
