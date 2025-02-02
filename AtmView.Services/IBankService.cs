using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBankService : IEntityService<Bank, int>
    {
        void Commit();
        void DeleteBank(int bankId, string ConnectionString);
    }
}
