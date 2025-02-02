using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class JournalEntryRepository : GenericRepo<JournalEntry>, IJournalEntryRepository
    {
        public JournalEntryRepository(AtmViewContext context)
            : base(context)
        {

        }




    }
}
