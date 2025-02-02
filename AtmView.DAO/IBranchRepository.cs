using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public interface IBranchRepository : IGenericRepository<Branch, int>
    {

        void DeleteBranch(int branchId, string ConnectionString);
    }
}
