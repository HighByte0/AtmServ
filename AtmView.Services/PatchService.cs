using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using iTextSharp.text;
using System.Collections.Generic;
using System.Linq;

namespace AtmView.Services
{
    public class PatchService : EntityService<Patch, int>,IPatchService
    {
        IUnitOfWork _unitOfWork;
        IPatchRepository _patchRepository;
        IPatchAtmRepository _patchAtmRepository;


        public PatchService(IUnitOfWork unitOfWork, IPatchRepository patchRepository, IPatchAtmRepository patchAtmRepository)
        : base(unitOfWork, patchRepository)
        {
            _unitOfWork = unitOfWork;
            _patchRepository = patchRepository;
            _patchAtmRepository = patchAtmRepository;

        }

        public IEnumerable<Patch> GetPatchesByAtmId(string atmId)
        {
            var patchIdsForAtm = _patchAtmRepository.GetPatchesByAtmId(atmId);

            return _patchRepository.GetPatchesByIds(patchIdsForAtm);
        }

        public List<Patch> GetAllPatches()
        {
            return _patchRepository.GetAll();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

    }
}
