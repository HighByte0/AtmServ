using AtmView.DAO.Common;
using AtmView.Entities;


namespace AtmView.DAO
{
    public class AtmCommErrorRepository :GenericRepository<AtmCommError, int>, IAtmCommErrorRepository
    {
         public AtmCommErrorRepository(AtmViewContext context)
            : base(context)
    {

    }
}
}
