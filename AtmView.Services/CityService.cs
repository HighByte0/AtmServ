using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class CityService : EntityService<City, int>, ICityService
    {
        IUnitOfWork _unitOfWork;
        ICityRepository _cityRepository;

        public CityService(IUnitOfWork unitOfWork, ICityRepository cityRepository)
            : base(unitOfWork, cityRepository)
        {
            _unitOfWork = unitOfWork;
            _cityRepository = cityRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }


        public void DeleteCity(int cityId, string ConnectionString)
        {
            _cityRepository.DeleteCity(cityId, ConnectionString);
        }

    }
}
