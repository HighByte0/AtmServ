using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ApplicationRepository : GenericRepository<Application, int>, IApplicationRepository
    {
        public ApplicationRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
