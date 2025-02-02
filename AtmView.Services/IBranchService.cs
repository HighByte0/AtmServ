using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBranchService : IEntityService<Branch, int>
    {
        void Commit();
        void DeleteBranch(int branchId, string ConnectionString);
    }
}
