using AtmView.Entities;

namespace AtmView.Services
{
    public interface IPredictorService : IEntityService<Predictor, int>
    {
        void Commit();
    }
}
