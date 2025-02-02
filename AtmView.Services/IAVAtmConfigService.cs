using AtmView.Entities;

namespace AtmView.Services
{
    public interface IAVAtmConfigService : IEntityService<AVAtmConfig, int>
    {
        void Commit();
    }
}
