using AtmView.Entities;

namespace AtmView.Services
{
    public interface IContactService : IEntityService<Contact, int>
    {
        void Commit();
    }
}
