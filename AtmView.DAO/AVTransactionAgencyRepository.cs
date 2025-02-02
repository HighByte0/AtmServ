using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{


    public class AVTransactionAgencyRepository : GenericRepo<AVTransactionAgency>, IAVTransactionAgencyRepository
    {
        public AVTransactionAgencyRepository(AtmViewContext context)
            : base(context)
        {

        }
    }


}

