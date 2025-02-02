using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;

namespace AtmView.Services
{
    public class AVAtmConfigService : EntityService<AVAtmConfig, int>, IAVAtmConfigService
    {
        IUnitOfWork _unitOfWork;
        IAVAtmConfigRepository _AVAtmConfigRepository;

        public AVAtmConfigService(IUnitOfWork unitOfWork, IAVAtmConfigRepository AVAtmConfigRepository)
            : base(unitOfWork, AVAtmConfigRepository)
        {
            _unitOfWork = unitOfWork;
            _AVAtmConfigRepository = AVAtmConfigRepository;


        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
