using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.DAO
{
    public class ComponentRepository : GenericRepository<Component, int>, IComponentRepository
    {
        public ComponentRepository(AtmViewContext context)
            : base(context)
        {

        }
    }
}

