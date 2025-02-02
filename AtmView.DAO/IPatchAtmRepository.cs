using AtmView.DAO.Common;
using AtmView.Entities;
using iTextSharp.text;
using System.Collections.Generic;

namespace AtmView.DAO
{
    public interface IPatchAtmRepository : IGenericRepository<PatchAtm, int>
    {
        IEnumerable<PatchAtm> GetPatchAtmsById(string atmId);

        List<int> GetPatchesByAtmId(string atmId);
        List<PatchAtm> GetAll();

    }
}
