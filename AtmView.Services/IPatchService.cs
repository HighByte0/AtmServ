using AtmView.Entities;
using System.Collections.Generic;

namespace AtmView.Services
{
    public interface IPatchService : IEntityService<Patch, int>
    {
        IEnumerable<Patch> GetPatchesByAtmId(string atmId);

        List<Patch> GetAllPatches();
        void Commit();

    }
}
