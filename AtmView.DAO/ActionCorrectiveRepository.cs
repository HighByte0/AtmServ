using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ActionCorrectiveRepository : GenericRepository<ActionCorrective, int>, IActionCorrectiveRepository
    {
        public ActionCorrectiveRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
