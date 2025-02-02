using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public interface IPatchAtmService : IEntityService<PatchAtm, int>
    {
        void Commit();
        IEnumerable<PatchAtm> GetPatchAtmsById(string atmId);

    }
}
