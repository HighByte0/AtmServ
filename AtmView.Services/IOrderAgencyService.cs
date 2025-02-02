using AtmView.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Services
{
    public interface IOrderAgencyService : IEntityService<OrderAgency, int>
    {
        void Commit();
    }
}
