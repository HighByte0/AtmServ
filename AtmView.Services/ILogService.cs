using AtmView.Entities;

namespace AtmView.Services
{
    public interface ILogService : IEntityService<Log, int>
    {

        void Commit();

        void AddLog(Log log);
    }
}
