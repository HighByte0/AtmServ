using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.DAO
{
    public class CassetteSetupRepository : GenericRepository<CassetteSetup, int>, ICassetteSetupRepository
    {
        public CassetteSetupRepository(AtmViewContext context)
            : base(context)
        {

        }
    
    }
}
