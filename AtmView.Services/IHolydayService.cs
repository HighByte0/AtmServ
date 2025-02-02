using AtmView.Entities;

namespace AtmView.Services
{
    public interface IHolydayService : IEntityService<Holyday, int>
    {
        void Commit();
    }
}
