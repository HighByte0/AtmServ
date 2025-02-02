using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ParametersRepository : GenericRepository<Parameters, int>, IParametersRepository
    {
        public ParametersRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
