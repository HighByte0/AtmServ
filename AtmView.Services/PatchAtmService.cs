using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public class PatchAtmService : EntityService<PatchAtm, int>, IPatchAtmService
    {
        IUnitOfWork _unitOfWork;
        IPatchAtmRepository _patchAtmRepository;

        public PatchAtmService(IUnitOfWork unitOfWork, IPatchAtmRepository patchAtmRepository)
        : base(unitOfWork, patchAtmRepository)
        {
            _unitOfWork = unitOfWork;
            _patchAtmRepository = patchAtmRepository;

        }

        public IEnumerable<PatchAtm> GetPatchAtmsById(string atmId)
        {
            return _patchAtmRepository.GetPatchAtmsById(atmId);
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
