using AtmView.DAO.Common;
using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.DAO
{
    public class ConfigCassetteRepository : GenericRepository<ConfigCassette, int>, IConfigCasseteRepository
    {
        public ConfigCassetteRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}
