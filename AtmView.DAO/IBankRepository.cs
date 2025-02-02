using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public interface IBankRepository : IGenericRepository<Bank, int>
    {

        void DeleteBank(int bankId, string ConnexionString);

    }
}
