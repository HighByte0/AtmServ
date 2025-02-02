using AtmView.Entities;

namespace AtmView.Services
{
    public interface IBugAttachmentService : IEntityService<BugAttachment, int>
    {
        void Commit();

        void Remove(BugAttachment bugAttachment);


    }
}
