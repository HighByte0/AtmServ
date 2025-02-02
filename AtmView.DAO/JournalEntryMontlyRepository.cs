using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class JournalEntryMontlyRepository : GenericRepo<JournalEntryMontly>, IJournalEntryMontlyRepository
    {
        public JournalEntryMontlyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
