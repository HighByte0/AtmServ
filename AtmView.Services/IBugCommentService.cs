using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugCommentService : IEntityService<BugComment, int>
    {
        void Commit();
        void Remove(BugComment bugComment);





    }

}
