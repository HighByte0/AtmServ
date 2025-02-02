using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Services
{
    public interface IAgencyService : IEntityService<Agency, string>
    {
        void Commit();
        void DeleteAgency(string agencyId, string ConnectionString);
    }
}
