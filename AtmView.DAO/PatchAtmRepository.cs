using AtmView.DAO.Common;
using AtmView.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AtmView.DAO
{
    public class PatchAtmRepository : GenericRepository<PatchAtm, int>, IPatchAtmRepository
    {
        private readonly AtmViewContext _context;

        public PatchAtmRepository(AtmViewContext context)
            : base(context)
        {
            _context = context;
        }

        public List<int> GetPatchesByAtmId(string atmId)
        {
            return _context.PatchAtm
                .Where(p => p.Atm_Id == atmId)
                .Select(p => p.Patch_Id ?? 0)
                .ToList();
        }

        public IEnumerable<PatchAtm> GetPatchAtmsById(string atmId)
        {
            return _context.PatchAtm.Where(p => p.Atm_Id == atmId).ToList();
        }

        public List<PatchAtm> GetAll()
        {
            return _context.PatchAtm.ToList();
        }
    }

}
