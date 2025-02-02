
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ErrorTypeRepository : GenericRepository<ErrorType, int>, IErrorTypeRepository
    {
        public ErrorTypeRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
