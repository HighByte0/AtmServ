using AtmView.Entities;

namespace AtmView.Services
{
    public interface IParametersService : IEntityService<Parameters, int>
    {
        void Commit();
    }
}
