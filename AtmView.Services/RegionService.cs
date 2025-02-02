using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class RegionService : EntityService<Region, int>, IRegionService
    {
        IUnitOfWork _unitOfWork;
        IRegionRepository _regionRepository;

        public RegionService(IUnitOfWork unitOfWork, IRegionRepository regionRepository)
            : base(unitOfWork, regionRepository)
        {
            _unitOfWork = unitOfWork;
            _regionRepository = regionRepository;

        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void DeleteRegion(int regionId, string ConnectionString)
        {
            _regionRepository.DeleteRegion(regionId, ConnectionString);
        }


    }
}
