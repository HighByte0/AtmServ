using AtmView.DAO;
using AtmView.DAO.Common;
using AtmView.Entities;
using iTextSharp.text;
using System.Collections.Generic;
using System.Linq;

namespace AtmView.DAO
{
    public class PatchRepository : GenericRepository<Patch, int>, IPatchRepository
    {
        private readonly AtmViewContext _context;

        readonly private IPatchAtmRepository _patchAtmRepository;


        public PatchRepository(AtmViewContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Patch> GetPatchesByIds(List<int> patchIds)
        {
            return _context.Patches.Where(p => patchIds.Contains(p.Id)).ToList();
        }


        public List<Patch> GetAll()
        {
            return _context.Patches.ToList();
        }
    }
}


