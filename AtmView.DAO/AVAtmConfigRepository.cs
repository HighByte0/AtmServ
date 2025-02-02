using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class AVAtmConfigRepository : GenericRepository<AVAtmConfig, int>, IAVAtmConfigRepository
    {
        public AVAtmConfigRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
