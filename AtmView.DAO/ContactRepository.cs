using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ContactRepository : GenericRepository<Contact, int>, IContactRepository
    {
        public ContactRepository(AtmViewContext context)
            : base(context)
        {

        }

        //public Contact GetCompleteContactById(int id)
        //{
        //    return _dbset.SingleOrDefault(x => x.Id.CompareTo(id) == 0);
        //}

    }
}

