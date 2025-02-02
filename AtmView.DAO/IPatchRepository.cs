using AtmView.DAO.Common;
using AtmView.Entities;
using iTextSharp.text;
using System.Collections.Generic;

namespace AtmView.DAO
{
    public interface IPatchRepository : IGenericRepository<Patch, int>
    {
        IEnumerable<Patch> GetPatchesByIds(List<int> patchIds);
        List<Patch> GetAll();

    }
}
