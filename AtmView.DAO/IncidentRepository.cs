using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class IncidentRepository : GenericRepository<Incident, int>, IIncidentRepository
    {
        public IncidentRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

